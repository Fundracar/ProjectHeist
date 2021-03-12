using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CharacterSettings _settings;
    [SerializeField] private GameObject _cam;
    [SerializeField] private UIProgressBar _progressBar;
    [SerializeField] private GameObject _interractiveImage;
    [SerializeField] private CanvasGroup _interractiveCanvasGroup;

    private CamManager _camManager;
    
    private float _currentProgress;

    public float CurrentProgress
    {
        
        
        get => _currentProgress;
        set 
        { 
            _currentProgress = value;
            _progressBar.CurrentValue = value;
        }
    }

    private Rigidbody _rb = default;

    private float _speed = default;
    public float Speed
    {
        set => _speed = value;
    }
    
    private Vector3 _playerAxesInput;
    private Vector3 _desiredVelocity;

    private Vector3 _velocity;
    
     private Vector3 _moveDirection;
     
    [Header("Interaction")]
    private bool _interactInput;
    private bool _canInteract;
    private InteractiveObject _interactive;
    
    [SerializeField] private List<Tools> _tools;
    [SerializeField] private Tools _equippedTool = default;

    [SerializeField] private GameObject _crew;
    
    

    private Bag _equippedBag;
    public Bag EquippedBag
    {
        get => _equippedBag;
        set => _equippedBag = value;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _speed = _settings.WalkSpeed;

        _interractiveCanvasGroup = _interractiveImage.GetComponent<CanvasGroup>();

        _camManager = _cam.GetComponent<CamManager>();
    }

    private void Start()
    {
        _equippedTool = GameManager.DictOfAllTools[PlayerPrefs.GetInt("toolId")];
        Debug.Log( $"{this.GetStamp()} Tools id : " + PlayerPrefs.GetInt("toolId"), this);
        GameManager.Instance.OnToolSwap(_equippedTool);

        _crew = GameManager.DictOfAllCrew[PlayerPrefs.GetInt("crewId")];
        Debug.Log($"{this.GetStamp()} CrewId : " + PlayerPrefs.GetInt("crewId"), this);
        
        _crew.GetComponent<Crew>().ActiveIt();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameRunning)
        {
            if (!Input.GetButton("Interact"))
            {
                // Handle movement
                _playerAxesInput.y = Input.GetAxis("Vertical");
                _playerAxesInput.x = Input.GetAxis("Horizontal");

                _moveDirection = (_playerAxesInput.x * _cam.transform.right +
                                  _playerAxesInput.y * _cam.transform.forward);

                _moveDirection = Vector3.ClampMagnitude(_moveDirection, 1f);
                _desiredVelocity = _moveDirection * _speed;
            }

            if (Input.GetButtonDown("Interact") && _canInteract)
                OnInteraction();


            // Change the speed of the character according to the entry on the Run or Crouch keys
            if (Input.GetButtonDown("Run"))
                _speed = _settings.RunSpeed;
            else if (Input.GetButtonDown("Crouch"))
                _speed = _settings.CrouchSpeed;
            else if (Input.GetButtonUp("Crouch") || Input.GetButtonUp("Run"))
                _speed = _settings.WalkSpeed;

            if (Input.GetKeyDown(KeyCode.W) && _equippedBag != null)
                DropBag();
            
            if (Input.GetMouseButtonDown(2))
                SwapTool();
        }
    }

    private void FixedUpdate()
    {
        if (!_camManager.IsMoving)
        {
            // Calculate and Apply the the velocity on the Character
            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, 2);
            _velocity.z = Mathf.MoveTowards(_velocity.z, _desiredVelocity.z, 2);

            _rb.velocity = _velocity;
        }

        if(_velocity.magnitude > 0) 
            transform.rotation = Quaternion.LookRotation(_velocity);
    }

    public void DropBag()
    {
        _equippedBag.transform.parent = null;
        _equippedBag.Collider.enabled = true;
        _equippedBag = null;
    }
    
    private void OnInteraction()
    {
        if (_interactive.NeedTool)
        {
            if (_equippedTool.ToolsId == _interactive.FirstToolId)
                StartCoroutine(UseTool());
            else if (_interactive.HaveSecondTool && _equippedTool.ToolsId == _interactive.SecondToolId)
                StartCoroutine(UseTool());
            else
                Debug.Log("Wrong tool");
        }
        else
            _interactive.OnInteraction();
    }

    //Use the equipped tool
    private IEnumerator UseTool()
    {
        if(_equippedTool.UseSprite != null)
             _progressBar.SwitchSprite(_equippedTool.UseSprite);
        
        _progressBar.gameObject.SetActive(true);
        _interractiveCanvasGroup.alpha = 0;
        
        CurrentProgress = 0;
        
        Debug.Log("Start use tool");
        float t = 0;
        while (t < _equippedTool.WaitTime)
        {
            if (Input.GetButtonUp("Interact"))
            {
                Debug.Log($"{this.SoundManagerStamp()} Stop use tool");
                _interractiveCanvasGroup.alpha = 1;
                _progressBar.gameObject.SetActive(false);
                yield break;
            }

            CurrentProgress += 1f / _equippedTool.WaitTime * Time.deltaTime;
            
            t += Time.deltaTime;
            yield return null;
        }

        _progressBar.gameObject.SetActive(false);
        
        GameManager.Instance.UpAnomaly(_equippedTool.AnomalyCost);
        _interractiveCanvasGroup.alpha = 1;
        
        Debug.Log("Finishing use tool");
        _interactive.OnInteraction();
    }
    
    private void OnGameOver()
    {
        _desiredVelocity = Vector2.zero;
        _rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactive") || other.CompareTag("Bag") || other.CompareTag("Door"))
        {
            _canInteract = true;
            _interactive = other.GetComponent<InteractiveObject>();
            _interractiveImage.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactive") || other.CompareTag("Bag") || other.CompareTag("Door"))
        {
            _canInteract = false;
            _interactive = null;
            _interractiveImage.SetActive(false);
        }
    }
    
    // Switch the equipped tool (Just for the Test)
    private void SwapTool()
    {
        int currIdx = _tools.IndexOf(_equippedTool);
        int nextIdx = (currIdx + 1) % _tools.Count;
        Tools nextTools = _tools[nextIdx];
        _equippedTool = nextTools;
      
        GameManager.Instance.OnToolSwap(_equippedTool);
    }
    
    private void OnEnable()
    {
        GameManager.gameOverEvt += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.gameOverEvt -= OnGameOver;
    }
}

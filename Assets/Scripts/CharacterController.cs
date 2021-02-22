using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CharacterSettings _settings;
    [SerializeField] private GameObject _cam;
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
        
    private Bag _equippedBag;
    public Bag EquippedBag
    {
        set => _equippedBag = value;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _speed = _settings.WalkSpeed;
        
        GameManager.Instance.OnToolSwap(_equippedTool);
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameRunning)
        {
            // Handle movement
            _playerAxesInput.y = Input.GetAxis("Vertical");
            _playerAxesInput.x = Input.GetAxis("Horizontal");
            
            _moveDirection = (_playerAxesInput.x * _cam.transform.right + _playerAxesInput.y * _cam.transform.forward);

            _moveDirection = Vector3.ClampMagnitude(_moveDirection, 1f);
            _desiredVelocity = _moveDirection * _speed;

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
            {
                _equippedBag.transform.parent = null;
                _equippedBag.Collider.enabled = true;
                _equippedBag = null;
            }
            
            if (Input.GetMouseButtonDown(2))
            {
                SwapTool();
            }
        }
    }

    private void FixedUpdate()
    {
        // Calculate and Apply the the velocity on the Character
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, 2);
        _velocity.z = Mathf.MoveTowards(_velocity.z, _desiredVelocity.z, 2);

        _rb.velocity = _velocity;
        
        if(_velocity.magnitude > 0) 
            transform.rotation = Quaternion.LookRotation(_velocity);
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
        {
            _interactive.OnInteraction();
        }
    }

    //Use the equipped tool
    private IEnumerator UseTool()
    {
        Debug.Log("Start use tool");
        float t = 0;
        while (t < _equippedTool.WaitTime)
        {
            if (_velocity.magnitude > 0)
            {
                Debug.Log("Stop use tool");
                yield break;
            }
            
            t += Time.deltaTime;
            yield return null;
        }

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
        if (other.CompareTag("Interactive") || other.CompareTag("Bag"))
        {
            _canInteract = true;
            _interactive = other.GetComponent<InteractiveObject>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactive") || other.CompareTag("Bag"))
        {
            _canInteract = false;
            _interactive = null;
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

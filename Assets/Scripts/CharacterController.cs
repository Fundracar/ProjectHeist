using System.Collections;
using System.Collections.Generic;
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

    private IEnumerator co;
    
    private Vector3 _playerAxesInput;
    private Vector3 _desiredVelocity;

    private Vector3 _velocity;

    private bool _interactInput;
    private bool _canInteract;

    private InteractiveObject _interactive;

    private Vector3 _moveDirection;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _speed = _settings.WalkSpeed;
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
                _interactive.OnInteraction();


            // Change the speed of the character according to the entry on the Run or Crouch keys
            if (Input.GetButtonDown("Run"))
                _speed = _settings.RunSpeed;
            else if (Input.GetButtonDown("Crouch"))
                _speed = _settings.CrouchSpeed;
            else if (Input.GetButtonUp("Crouch") || Input.GetButtonUp("Run"))
                _speed = _settings.WalkSpeed;
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
    
    private void OnGameOver()
    {
        _desiredVelocity = Vector2.zero;
        _rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactive"))
        {
            _canInteract = true;
            _interactive = other.GetComponent<InteractiveObject>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactive"))
        {
            _canInteract = false;
            _interactive = null;
        }
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

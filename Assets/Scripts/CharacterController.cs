using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
     [SerializeField] private CharacterSettings _settings;

    private Rigidbody _rb = default;
    
    private float _speed = default;
    public float Speed
    {
        set => _speed = value;
    }
    
    private Vector2 _playerAxesInput;
    private Vector2 _desiredVelocity;

    private Vector3 _velocity;

    private bool _interactInput;
    private bool _canInteract;

    private InteractiveObject _interactive;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _speed = _settings.WalkSpeed;
    }

    private void Update()
    {
        // Handle movement
        _playerAxesInput.y = Input.GetAxis("Vertical");
        _playerAxesInput.x = Input.GetAxis("Horizontal");
        
        _playerAxesInput = Vector2.ClampMagnitude(_playerAxesInput, 1f);
        _desiredVelocity = _playerAxesInput * _speed;

        if (Input.GetButtonDown("Interact") && _canInteract)
            _interactive.OnInteraction();
        

        // Change the speed of the character according to the entry on the Run or Crouch keys
        if (Input.GetButtonDown("Run"))
            _speed = _settings.RunSpeed;
        else if(Input.GetButtonDown("Crouch"))
            _speed = _settings.CrouchSpeed;
        else if(Input.GetButtonUp("Crouch") || Input.GetButtonUp("Run"))
            _speed = _settings.WalkSpeed;
    }

    private void FixedUpdate()
    {
        // Calculate and Apply the the velocity on the Character
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, 1);
        _velocity.z = Mathf.MoveTowards(_velocity.z, _desiredVelocity.y, 1);

        _rb.velocity = _velocity;
        
        if(_velocity.magnitude > 0) 
            transform.rotation = Quaternion.LookRotation(_velocity);
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
}

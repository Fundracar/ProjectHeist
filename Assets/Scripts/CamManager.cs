using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    private Transform _tr;
    [SerializeField] private Transform _characterTr = default;

    [SerializeField] private List<Vector3> _position;
    [SerializeField] private int _positionIndex = 0;

    [SerializeField] private float _orbitSpeed = 1f;

    private bool _isMoving = false;
    public bool IsMoving => _isMoving;
    
    private Vector3 _actualPosition;
    private Vector3 _newPosition;
    
    void Awake()
    {
        _tr = GetComponent<Transform>();
    }

    private void Start()
    {
        _actualPosition = _position[_positionIndex];
        _newPosition = _actualPosition + _characterTr.position;
        _tr.position = _newPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isMoving)
        {
            _newPosition = _actualPosition + _characterTr.position;
            _tr.position = _newPosition;
        }

        if (Input.GetKeyDown(KeyCode.A) && !_isMoving)
            StartCoroutine(Rotate(1));
        else if (Input.GetKeyDown(KeyCode.E) && !_isMoving)
            StartCoroutine(Rotate(-1));
    }

    //Change the angle the view
    private IEnumerator Rotate(int direction)
    {
        _isMoving = true;
        _characterTr.GetComponent<Rigidbody>().velocity = Vector3.zero;

        yield return null;
        
        float target = _tr.eulerAngles.y + direction * 90;
        
        if (target == -90)
            target = 270;
        else if (target == 360)
            target = 0;
        
       

        if (_positionIndex == 3 && direction == 1)
            _positionIndex = 0;
        else if (_positionIndex == 0 && direction == -1)
            _positionIndex = 3;
        else
            _positionIndex += direction;

        _actualPosition = _position[_positionIndex];
        
        
        while (Math.Abs(transform.eulerAngles.y - target) > 0.1)
        {
            _tr.RotateAround(_characterTr.position, Vector3.up,direction * _orbitSpeed);
            
            var lookPos = _characterTr.position - _tr.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            
            _tr.rotation = Quaternion.Slerp(_tr.rotation, rotation, Time.deltaTime * _orbitSpeed);
            yield return new WaitForFixedUpdate();
        }

        _isMoving = false;

        _tr.rotation = Quaternion.Euler(0, target, 0);
    }
    
}

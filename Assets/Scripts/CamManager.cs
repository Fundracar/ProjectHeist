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
    
    private Vector3 _actualPosition;
    private Vector3 _newPosition;
    
    void Awake()
    {
        _tr = GetComponent<Transform>();
    }

    private void Start()
    {
        _actualPosition = _position[_positionIndex];
    }

    // Update is called once per frame
    void Update()
    {
        _newPosition = _actualPosition + _characterTr.position;
        _tr.position = _newPosition;

        if (Input.GetKeyDown(KeyCode.A))
            Rotate(1);
        else if(Input.GetKeyDown(KeyCode.E))
            Rotate(-1);
    }

    //Change the angle the view
    private void Rotate(int direction)
    {
        float target = _tr.rotation.eulerAngles.y + direction * 90;
        _tr.rotation = Quaternion.Euler(0, target, 0);

        if (_positionIndex == 3 && direction == 1)
            _positionIndex = 0;
        else if (_positionIndex == 0 && direction == -1)
            _positionIndex = 3;
        else
            _positionIndex += direction;

        _actualPosition = _position[_positionIndex];
    }
    
}

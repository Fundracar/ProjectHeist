using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    private Transform _tr;
    [SerializeField] private Transform _characterTr = default;

    private Vector3 _startedPosition;
    private Vector3 _newPosition;
    
    void Awake()
    {
        _tr = GetComponent<Transform>();
        _startedPosition = _tr.position;
    }

    // Update is called once per frame
    void Update()
    {
        _newPosition.y = _startedPosition.y;
        _newPosition.z = _startedPosition.z + _characterTr.position.z;
        _tr.position = new Vector3(_characterTr.position.x, _newPosition.y, _newPosition.z);
    }
}

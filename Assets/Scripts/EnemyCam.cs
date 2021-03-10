using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCam : Enemy
{
    [SerializeField] private bool isRotate;
    [SerializeField] private float rotationAmplitude;
    private bool _isLeft;
    
    public enum ePos
    {
        Left,
        Start,
        Right
    }

    private ePos _pos;

    private Quaternion _startQuaternion;
    private Quaternion _leftQuaternion;
    private Quaternion _rightQuaternion;

    private Quaternion _nextQuaternion;
    
    private void Start()
    {
        _startQuaternion = GetComponent<Transform>().rotation;
        _rightQuaternion = Quaternion.Euler(_startQuaternion.eulerAngles.x,_startQuaternion.eulerAngles.y + rotationAmplitude, 0);
        _leftQuaternion = Quaternion.Euler(_startQuaternion.eulerAngles.x,_startQuaternion.eulerAngles.y - rotationAmplitude , 0);
        
        _nextQuaternion = _rightQuaternion;
        
        if (isRotate)
            StartCoroutine(Rotate());

        
    }
    
    
    protected override IEnumerator Rotate( )
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, _nextQuaternion, t );

            yield return new WaitForFixedUpdate();
            yield return null;
        }
        
        if (_pos != ePos.Start)
            GoStart();
        else if(_isLeft)
            GoRight();
        else
            GoLeft();
    }

    private void GoRight()
    {
        _nextQuaternion = _rightQuaternion;
        StartCoroutine(Rotate());
        _isLeft = false;
        _pos = ePos.Right;

    }

    private void GoStart()
    {
        _nextQuaternion = _startQuaternion;
        StartCoroutine(Rotate());
        _pos = ePos.Start;
    }

    private void GoLeft()
    {
        _nextQuaternion = _leftQuaternion;
        StartCoroutine(Rotate());
        _isLeft = true;
        _pos = ePos.Left;
    }

    public void SetActiveFalse()
    {
        _fov.enabled = false;
        this.enabled = false;
    }
}

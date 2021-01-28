using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;

public class Enemy : MonoBehaviour
{
    private float _detectionPercentage;

    [SerializeField] protected EnemySettings _settings;

    int layerMask = 1 << 8;

    private Transform _characterTr;
    private Vector3 _characterPos;
    private Vector3 _toCharacter;
    private bool _characterInPoV;

    private void Awake()
    {
        _characterTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Move();
        DetectCharacter();
    }

    protected virtual void Move()
    {
        
    }

    void DetectCharacter()
    {
        _detectionPercentage = Mathf.Clamp(_detectionPercentage, 0, 100);
        _toCharacter = _characterTr.position - transform.position;
        
        if (!_characterInPoV && _detectionPercentage > 0)
            DecreasedPercentage();
        else if (_characterInPoV)
            IncreasedPercentage();
    }

    private void DecreasedPercentage()
    {
        _detectionPercentage -= Time.deltaTime;
    }

    private void IncreasedPercentage()
    {
        if (CalculateAngle() <= _settings.AngleDetection / 2)
        {
             RaycastHit hit;
             Physics.Raycast(transform.position, _toCharacter.normalized, out hit, _settings.DistanceView, layerMask);
             Debug.DrawRay(transform.position, _toCharacter.normalized * _settings.DistanceView, Color.red);
             
             if (hit.collider.CompareTag("Player"))
                 _detectionPercentage += Time.deltaTime;
        }
        else
        {
            DecreasedPercentage();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _characterInPoV = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _characterInPoV = false;
    }

    private float CalculateAngle()
    {
        float angle = Vector3.Angle(transform.forward, _toCharacter);

        return angle;
    }
}

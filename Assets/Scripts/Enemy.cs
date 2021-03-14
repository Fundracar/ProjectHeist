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

    private Transform _tr;

    protected int layerMask = 1 << 8;

    protected enum States
    {
        Patrol,
        Chase,
        Search
    }
    protected States _states = 0;
    
    private Transform _characterTr;
    protected Vector3 _characterPos;
    private Vector3 _toCharacter;
    protected bool _characterInFoV;
    
    protected Light _fov;

    private bool anomalyUp = false;

    private void Awake()
    {
        _tr = GetComponent<Transform>();
        
        _characterTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        _fov = GetComponent<Light>();
        _fov.spotAngle = _settings.AngleDetection;
        _fov.range = _settings.DistanceView;

       GetComponent<CapsuleCollider>().radius = _settings.DistanceView;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameRunning)
            ChangeDetectPercentage();
    }

    // Move the Entity
    protected virtual IEnumerator Move()
    {
        yield return null;
    }

    protected virtual IEnumerator Rotate()
    {
        yield return null;
    }
    
    private void ChangeDetectPercentage()
    {
        // Update character position and the distance between the Enemy and the Player
        _characterPos = _characterTr.position;
        _toCharacter = _characterPos - transform.position;

        float dist = _toCharacter.sqrMagnitude;
        
        if (!_characterInFoV && _detectionPercentage > 0)
            DecreasedPercentage();
        else if (_characterInFoV && CalculateAngle() <= _settings.AngleDetection/2)
        {
            if (FindPlayer())
            {
                //IncreasedPercentage();

                GameManager.Instance.GameOver();
                
               /* if (_states == States.Patrol && _detectionPercentage > _settings.StartInvestigatePercentage)
                    StartCoroutine(InvestigateAction());*/
            }
           /* else
                DecreasedPercentage();*/
        }
    }

    // Detects the Player 
    protected bool FindPlayer()
    {
        bool success = false;
        
        RaycastHit hit;
        Physics.Raycast(transform.position, _toCharacter.normalized, out hit, _settings.DistanceView +1, layerMask);
        Debug.DrawRay(transform.position, _toCharacter.normalized * _settings.DistanceView, Color.red);
        
        if (hit.collider.CompareTag("Player"))
                success = true;

        return success;
    }

    // Decrease the detection percentage
    private void DecreasedPercentage(){
        
        _detectionPercentage -= Time.deltaTime * _settings.DetectionSpeed;
        _detectionPercentage = Mathf.Clamp(_detectionPercentage, 0, 100);

        if (_detectionPercentage == 0)
            anomalyUp = false;
    }

    // Increase the detection percentage
    private void IncreasedPercentage()
    { 
        _detectionPercentage += Time.deltaTime * _settings.DetectionSpeed;
        _detectionPercentage = Mathf.Clamp(_detectionPercentage, 0, 100);
        
        if (_detectionPercentage >= 100)
            GameManager.Instance.GameOver();

        if (_detectionPercentage > 75 && !anomalyUp)
        {
            anomalyUp = true;
            GameManager.Instance.UpAnomaly(10);
        }
    }

    // Active a action when the Player is detected
    protected virtual IEnumerator InvestigateAction()
    {
        yield return null;
    }
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _characterInFoV = true;
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _characterInFoV = false;
    }

    protected float CalculateAngle()
    {
        float angle = Vector3.Angle(transform.forward, _toCharacter);
        return angle;
    }
}

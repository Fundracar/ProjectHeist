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

    protected IEnumerator cor;
    protected IEnumerator move;
    
    private Light _fov;

    private void Awake()
    {
        _characterTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        _fov = GetComponent<Light>();
        _fov.spotAngle = _settings.AngleDetection;
        _fov.range = _settings.DistanceView;
    }

    protected void Start()
    {
        StartCoroutine(Move());
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameRunning)
            ChangeDetectPercentage();
    }

    // Move the this Entity
    protected virtual IEnumerator Move()
    {
        yield return null;
    }

    protected virtual IEnumerator Rotate()
    {
        yield return null;
    }
    
    //TODO Voir avec le prof si + opti possible
    // Detects the Player 
    private void ChangeDetectPercentage()
    {
        // Update character position and the distance between the Enemy and the Player
        _characterPos = _characterTr.position;
        _toCharacter = _characterPos - transform.position;
        
        if (!_characterInFoV && _detectionPercentage > 0)
            DecreasedPercentage();
        else if (_characterInFoV)
        {
            if (CalculateAngle() <= _settings.AngleDetection / 2)
            {
                if (SearchPlayer().collider.CompareTag("Player"))
                {
                    IncreasedPercentage();
                    
                    if(_states == States.Patrol && _detectionPercentage > _settings.StartInvestigatePercentage)
                        StartCoroutine(InvestigateAction());
                }
            }
            else
                DecreasedPercentage();
        }
    }

    protected RaycastHit SearchPlayer()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, _toCharacter.normalized, out hit, _settings.DistanceView, layerMask);
        Debug.DrawRay(transform.position, _toCharacter.normalized * _settings.DistanceView, Color.red);
        
        return hit;
    }

    // Decrease the detection percentage
    private void DecreasedPercentage(){
        
        _detectionPercentage -= Time.deltaTime * _settings.DetectionSpeed;
        _detectionPercentage = Mathf.Clamp(_detectionPercentage, 0, 100);
    }

    // Increase the detection percentage
    private void IncreasedPercentage()
    { 
        _detectionPercentage += Time.deltaTime * _settings.DetectionSpeed;
        _detectionPercentage = Mathf.Clamp(_detectionPercentage, 0, 100);
        
        if (_detectionPercentage == 100)
            GameManager.Instance.GameOver();
    }

    // Active a action when the Player is detected
    protected virtual IEnumerator InvestigateAction()
    {
        yield return null;
    }
    
    private void OnTriggerEnter(Collider other)
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

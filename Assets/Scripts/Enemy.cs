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
    protected Vector3 _characterPos;
    private Vector3 _toCharacter;
    protected bool _characterInFoV;

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
            DetectCharacter();
    }

    // Move the this Entity
    protected virtual IEnumerator Move()
    {
        yield return null;
    }

    // Detects the Player 
    private void DetectCharacter()
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
                RaycastHit hit;
                Physics.Raycast(transform.position, _toCharacter.normalized, out hit, _settings.DistanceView, layerMask);
                Debug.DrawRay(transform.position, _toCharacter.normalized * _settings.DistanceView, Color.red);

                if (hit.collider.CompareTag("Player"))
                {
                    IncreasedPercentage();
                    StartCoroutine(InvestigateAction());
                }
            }
            else
                DecreasedPercentage();
        }
    }

    // Decrease the detection percentage
    private void DecreasedPercentage()
    {
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

    private float CalculateAngle()
    {
        float angle = Vector3.Angle(transform.forward, _toCharacter);
        return angle;
    }
}

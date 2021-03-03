using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Heist/Entity/Enemy", order = 0)]
public class EnemySettings : ScriptableObject
{
    [Tooltip("Move Speed")][Range(0.1f,10)][SerializeField] private float _speed;
    public float Speed => _speed;
    
    [Tooltip("Waiting time before moving to the next destination (in second)")][Range(0,10)][SerializeField] private float _waitTime;
    public float WaitTime => _waitTime;
    
    [Header("Detection Settings")]
    
    [Range(1,360)][SerializeField] private int _angleDetection;
    public int AngleDetection => _angleDetection;

    [Range(0.1f, 20)][SerializeField] private float _distanceView;
    public float DistanceView => _distanceView;

    [Range(1, 10)] [SerializeField] private float _detectionSpeed;
    public float DetectionSpeed => _detectionSpeed;

    [Range(10, 30)] [SerializeField] private float _startInvestigatePercentage;
    public float StartInvestigatePercentage => _startInvestigatePercentage;
}

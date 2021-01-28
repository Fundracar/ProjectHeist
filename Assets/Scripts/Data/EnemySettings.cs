using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Heist/Entity/Enemy", order = 0)]
public class EnemySettings : ScriptableObject
{
    [Range(1,360)][SerializeField] private int _angleDetection;
    public int AngleDetection => _angleDetection;
    
    [Range(0.1f,10)][SerializeField] private float _speed;
    public float Speed => _speed;

    [Range(0.1f, 10)][SerializeField] private float _distanceView;
    public float DistanceView => _distanceView;

    [Range(1, 10)] [SerializeField] private float _detectionSpeed;
    public float DetectionSpeed => _detectionSpeed;
    
}

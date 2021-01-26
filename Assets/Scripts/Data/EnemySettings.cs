using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Heist/Settings/Enemy", order = 0)]
public class EnemySettings : ScriptableObject
{
    [Range(0,360)][SerializeField] private int _angleDetection;
    
    [Range(0,100)] [SerializeField] private float _speed;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPointData", menuName = "Heist/Settings/SpawnPoint", order = 0)]
public class SpawnPointData : ScriptableObject
{
    [SerializeField] private GameObject _point = default;
    public GameObject Point => _point;
    
    [SerializeField] private GameObject _guardPrefab;
    public GameObject GuardPrefab => _guardPrefab;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPointData", menuName = "Heist/Settings/SpawnPoint", order = 0)]
public class SpawnPointData : ScriptableObject
{
    [SerializeField] private GameObject _point = default;
    public GameObject Point => _point;
    
    [Tooltip("The prefab of the Entity it needs to spawn")][SerializeField] private GameObject _guardPrefab;
    public GameObject GuardPrefab => _guardPrefab;
}

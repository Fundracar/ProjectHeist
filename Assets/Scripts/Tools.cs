using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tools", menuName = "Heist/item/Tool", order = 0)]
public class Tools: ScriptableObject
{
    [Range(100,200)][SerializeField] private int _toolsId;
    public int ToolsId => _toolsId;

    [SerializeField] private int _anomalyCost;
    public int AnomalyCost => _anomalyCost;

    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private float _waitTime;
    public float WaitTime => _waitTime;
}

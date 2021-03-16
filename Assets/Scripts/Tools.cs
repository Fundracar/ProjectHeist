using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Tools", menuName = "Heist/item/Tool", order = 0)]
public class Tools: ScriptableObject
{
    [Range(100,199)][SerializeField] private int _toolsId;
    public int ToolsId => _toolsId;

    [SerializeField] private int _anomalyCost;
    public int AnomalyCost => _anomalyCost;

    [SerializeField] private Sprite _selectedSprite;
    public Sprite SelectedSprite => _selectedSprite;
    
    [SerializeField] private Sprite _unlockedSprite;
    public Sprite UnlockedSprite => _unlockedSprite;

    [SerializeField] private Sprite _useSprite;
    public Sprite UseSprite => _useSprite;

    [SerializeField] private float _waitTime;
    public float WaitTime => _waitTime;

    [SerializeField] private int _toolReputationTreshold;
    public int ToolReputationTreshold => _toolReputationTreshold;

    [SerializeField] public string toolDescription;
    [SerializeField] public string toolEffect;
    [SerializeField] public string toolName;
}

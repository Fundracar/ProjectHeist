using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Crew : MonoBehaviour
{
    [SerializeField][Range(200,299)] private int crewId;
    public int CrewId => crewId;
    
    [SerializeField] private int crewReputationTreshold;
    public int CrewReputationTreshold => crewReputationTreshold;
    
    [SerializeField] private Sprite crewSprite;
    public Sprite CrewSprite => crewSprite;

    [SerializeField] private bool isBonus;
    public bool IsBonus => isBonus;

    [SerializeField][Range(0,100)] private int _anomalyCost;
    public int AnomalyCost => _anomalyCost;
    
    public abstract void ActiveIt();
}

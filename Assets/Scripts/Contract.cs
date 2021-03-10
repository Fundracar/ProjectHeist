using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contract : MonoBehaviour
{
    public string contractName;
    public int contractId;
    public int vigilanceScore;
    
    [SerializeField] private string _sceneName;
    public string SceneName => _sceneName;

    [Header("MainGoal")] 
    [SerializeField] private int _nbOfMainObjectives;
    public int NbOfMainObjectives => _nbOfMainObjectives;
    
    [SerializeField] private string contractMainObjective;
    public string ContractMainObjective => contractMainObjective;
    
    [Header("BonusGoal")]
    [SerializeField] private int _nbOfBonusObjectives;
    public int NbOfBonusObjectives => _nbOfBonusObjectives;
    
    [SerializeField] private string contractBonusObjective;
    public string ContractBonusObjective => contractBonusObjective;
    
    [Header("Reward")]
    [SerializeField] private int reputationBaseReward;
    public int ReputationBaseReward => reputationBaseReward;
    
    [SerializeField] private int moneyBaseReward;
    public int MoneyBaseReward => moneyBaseReward;
    
    [SerializeField] private int moneyBonusReward;
    public int MoneyBonusReward => moneyBonusReward;
    
    [SerializeField] private int reputationBonusReward;
    public int ReputationBonusReward => reputationBonusReward;
    
    [Header("Info")]
    [SerializeField] private int reputationTreshold;
    public int ReputationTreshold => reputationTreshold;
    
    [SerializeField] private string contractDescription;
    public string ContractDescription => contractDescription;

    [SerializeField] private string contractConstraints;
    public string ContractConstraints => contractConstraints;

    
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private bool _isGameRunning;
    public bool IsGameRunning => _isGameRunning;

    private int _anomaly;

    [Header("Tools")]
    [SerializeField] private List<Tools> listOfToolsPrefab;
    private static Dictionary<int, Tools> _dictOfAllTools;
    public static Dictionary<int, Tools> DictOfAllTools => _dictOfAllTools;
    [SerializeField] private int necessaryToolId;

    [Header("Crew")]
    [SerializeField] private List<Crew> listOfCrewPrefab;
    private static Dictionary<int, Crew> _dictOfAllCrew;
    public static Dictionary<int, Crew> DictOfAllCrew => _dictOfAllCrew;
    
    public static event Action gameOverEvt;
    public static event Action onVictoryEvt; 
    public static event Action<Tools> toolSwapEvt;

    [SerializeField]private Contract _contract;
    public Contract Contract
    {
        get => _contract;
        set => _contract = value;
    }

   /* private int _bonusReward;
    public int BonusReward
    {
        get => _bonusReward;
        set => _bonusReward = value;
    }
*/
    private int _totalReward = 0;
    public int TotalReward
    {
        get => _totalReward;
        set
        {
            _totalReward += value;
        } 
    }

    void Awake()
    {
        _isGameRunning = true;

        _dictOfAllTools = ToolsDictionary.InitializeDict(listOfToolsPrefab);
        _dictOfAllCrew = CrewDict.InitializeDict(listOfCrewPrefab);
        
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogWarning("instance already created");
            Destroy(this);
            return;
        }

        TotalReward = Contract.moneyBaseReward;
        TotalReward = Contract.moneyBonusReward;
    }

    private void Start()
    {
        SaveManager.LoadSave();
    }

    public void GameOver()
    {
        _isGameRunning = false;
        Debug.Log("Game Over!");
        
        if (gameOverEvt != null) 
            gameOverEvt();
        else
            Debug.LogWarning("Stop! No subscriptions");
    }

    public void UpAnomaly(int i)
    {
        _anomaly += i;

        if (_anomaly >= 100)
            ActiveAlert();
    }

    private void ActiveAlert()
    {
        Debug.Log("Alarm Activated");
    }
    
    public void OnToolSwap(Tools tools)
    {
        if (toolSwapEvt != null)
            toolSwapEvt.Invoke(tools);
    }

    public void OnVictory()
    {
        _isGameRunning = false;
        
        if(onVictoryEvt != null)
            onVictoryEvt();
    }
}

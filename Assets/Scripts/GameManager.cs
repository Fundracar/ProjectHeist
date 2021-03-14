using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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
    [SerializeField] private List<GameObject> listOfCrewPrefab;
    private static Dictionary<int, GameObject> _dictOfAllCrew;
    public static Dictionary<int, GameObject> DictOfAllCrew => _dictOfAllCrew;

    private float _moneyBonus = 0;
    public float MoneyBonus
    {
        get => _moneyBonus;
        set => _moneyBonus = value;
    }
    
    public static event Action gameOverEvt;
    public static event Action onVictoryEvt; 
    public static event Action<Tools> toolSwapEvt;

    [SerializeField]private Contract _contract;
    public Contract Contract => _contract;
    
    private List<EnemyCam> _enemyCams;
    public List<EnemyCam> EnemyCams => _enemyCams;

    private List<Guard> _guards;
    public List<Guard> Guards
    {
        get => _guards;
        set => _guards = value;
    }

    [SerializeField] private List<Door> _doors;

    public List<Door> Doors => _doors;
    

    private bool _mainGoalCompleted;

    public bool MainGoalCompleted
    {
        get => _mainGoalCompleted;
        set => _mainGoalCompleted = value;
    }
    private int _mainGoalProgression;
    public int MainGoalProgression
    {
        get => _mainGoalProgression;
        set
        {
            _mainGoalProgression++;
            if (_mainGoalProgression == Contract.NbOfMainObjectives)
                _mainGoalCompleted = true;
        } 
    }

    private bool _bonusGoalCompleted;
    public bool BonusGoalCompleted=> _bonusGoalCompleted;
    
    private int _bonusGoalProgression;
    public int BonusGoalProgression
    {
        get => _bonusGoalProgression;
        set
        {
            _bonusGoalProgression++;
            if (_bonusGoalProgression == Contract.NbOfBonusObjectives)
                _bonusGoalCompleted = true;
        } 
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
            _instance = this;
        else
        {
            Debug.LogWarning($"{this.GameManagerStamp()} instance already created");
            Destroy(this);
            return;
        }

        _enemyCams = new List<EnemyCam>(FindObjectsOfType<EnemyCam>());
        _doors = new List<Door>(FindObjectsOfType<Door>());
        _guards = new List<Guard>(FindObjectsOfType<Guard>());
        /*TotalReward = Contract.moneyBaseReward;
        TotalReward = Contract.moneyBonusReward;*/
    }

    private void Start()
    {
        SaveManager.LoadSave();
    }

    public void GameOver()
    {
        _isGameRunning = false;
        Debug.Log($"{this.GameManagerStamp()}Game Over!", this);
        
        if (gameOverEvt != null) 
            gameOverEvt();
        else
            Debug.LogWarning("Stop! No subscriptions");
    }

    public void UpAnomaly(int i)
    {
        _anomaly += i;
        UIAnomalyBar.Instance.CurrentValue = _anomaly;

        Debug.Log($"{this.GameManagerStamp()} Up Anomaly (anomaly + " + i + ")", this);
        
        if (_anomaly >= 100)
            ActiveAlert();
    }

    private void ActiveAlert()
    {
        Debug.Log($"{this.GameManagerStamp()}Alarm Activated");
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance => _instance;

    [SerializeField] private bool _resetSave;
    
    [SerializeField] private int _money;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Debug.LogWarning($"{this.GetStamp()} instance already created");
            Destroy(this);
            return;
        }
    }

    private void Update()
    {
        if (_resetSave)
            Reset();
    }

    public int Money
    {
        get => _money;
        set => _money += value;
    }

    private void Reset()
    {
        _resetSave = false;
        _playerRep = 1;
        _money = 0;
        
        SaveManager.SaveSave();
    }
    
   [SerializeField] private int _playerRep;
    public int PlayerRep
    {
        get => _playerRep;
        set => _playerRep += value;
    }

    private void OnSaveLoaded(Save save)
    {
        _money = save.money;
        _playerRep = save.playerRep;
    }

    private void OnSavePreparing(Save save)
    {
        save.playerRep = _playerRep;
        save.money = _money;
    }

    private void OnVictory()
    {
        Money = (int) (GameManager.Instance.Contract.MoneyBaseReward * (1f +  GameManager.Instance.MoneyBonus /100));
        if (GameManager.Instance.BonusGoalCompleted)
            Money += (int) (GameManager.Instance.Contract.MoneyBonusReward * (1f +  GameManager.Instance.MoneyBonus /100));
        
        SaveManager.SaveSave();
    }
    
    private void OnEnable()
    {
        SaveManager.saveLoaedEvt += OnSaveLoaded;
        SaveManager.savePreparingEvt += OnSavePreparing;
        GameManager.onVictoryEvt += OnVictory;
    }

    private void OnDisable()
    {
        if (SaveManager.saveLoaedEvt != null) 
            SaveManager.saveLoaedEvt -= OnSaveLoaded;
        if (SaveManager.savePreparingEvt != null) 
            SaveManager.savePreparingEvt -= OnSavePreparing;
            
        GameManager.onVictoryEvt -= OnVictory;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;
    
    private int _money;
    public int Money
    {
        get => _money;
        set => _money += value;
    }

   [SerializeField] private int _playerRep;
    public int PlayerRep
    {
        get => _playerRep;
        set => _playerRep += value;
    }

    public void OnSaveLoaded(Save save)
    {
        _money = save.money;
        _playerRep = save.playerRep;
    }

    public void OnSavePreparing(Save save)
    {
        save.playerRep = _playerRep;
        save.money = _money;
    }

    private void OnVictory()
    {
        Money = GameManager.Instance.TotalReward;
        
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

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIObjective : MonoBehaviour
{
    private static UIObjective _instance;
    public static UIObjective Instance => _instance;
    
    [SerializeField] private TMP_Text _mainObjective;
    [SerializeField] private TMP_Text _bonusObjective;

    private void Start()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Debug.LogWarning($"{this.GameManagerStamp()} instance already created");
            Destroy(this);
            return;
        }
        
        InitializedUI();
    }

    private void InitializedUI()
    {
        _mainObjective.text = "0/" + GameManager.Instance.Contract.NbOfMainObjectives;
        _bonusObjective.text = "0/" + GameManager.Instance.Contract.NbOfBonusObjectives;
    }

    public void UpdateUI(bool isMain)
    {
        if (isMain)
        {
             _mainObjective.text = GameManager.Instance.MainGoalProgression + "/" + GameManager.Instance.Contract.NbOfMainObjectives;
             if(GameManager.Instance.MainGoalCompleted)
                 _mainObjective.color = Color.green;
        }
        else
        {
            _bonusObjective.text =  GameManager.Instance.BonusGoalProgression + "/" + GameManager.Instance.Contract.NbOfBonusObjectives;
            if(GameManager.Instance.BonusGoalCompleted)
                _bonusObjective.color = Color.green;
        }
            
    }
}

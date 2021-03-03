using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIReward : MonoBehaviour
{
    [SerializeField] private TMP_Text _contractRewardTMP;
    [SerializeField] private TMP_Text _bonusRewardTMP;
    [SerializeField] private TMP_Text _AnomalyRewardTMP;
    [SerializeField] private TMP_Text _totalRewardTMP;

    private void OnVictory()
    {
        _bonusRewardTMP.text = GameManager.Instance.Contract.moneyBonusReward + "$";
        _contractRewardTMP.text =  "+" + GameManager.Instance.Contract.moneyBaseReward + "$";
        _AnomalyRewardTMP.text = "+ 0$";
        _totalRewardTMP.text = "=" + GameManager.Instance.TotalReward + "$";
    }

    private void OnEnable()
    {
        GameManager.onVictoryEvt += OnVictory;
    }

    private void OnDisable()
    {
        GameManager.onVictoryEvt -= OnVictory;
    }
}

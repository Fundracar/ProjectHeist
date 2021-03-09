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
        _contractRewardTMP.text =  "+" + GameManager.Instance.Contract.MoneyBaseReward + "$";
        _AnomalyRewardTMP.text = "x" +  (1f + GameManager.Instance.MoneyBonus / 100);

        if (GameManager.Instance.BonusGoalCompleted)
        {
            _bonusRewardTMP.text = GameManager.Instance.Contract.MoneyBonusReward + "$";
            _totalRewardTMP.text =
                "=" + GameManager.Instance.TotalReward * (1f + GameManager.Instance.MoneyBonus / 100) + "$";
        }
        else
        {
            _bonusRewardTMP.text = "+0$";
            _totalRewardTMP.text = "=" + GameManager.Instance.Contract.MoneyBaseReward *
                (1f + GameManager.Instance.MoneyBonus / 100) + "$";
        }
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

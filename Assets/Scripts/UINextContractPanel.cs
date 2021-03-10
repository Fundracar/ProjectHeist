using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINextContractPanel : MonoBehaviour
{
    [SerializeField] private GameObject _planningPanel;
    [SerializeField] private GameObject _errorLog;
    private TMP_Text errorTMP;

    private const string _noEnoughtRep = "Not enough reputation !";
    private const string _noContractSelected = "Please selecte a contract !";

    private void Awake()
    {
        errorTMP = _errorLog.GetComponentInChildren<TMP_Text>();
    }

    public void OnClick()
    {
        if (TempGameManager.Instance.storedContract == null)
            StartCoroutine(NotThisContract(_noContractSelected));
        else if (TempGameManager.Instance.storedContract.ReputationTreshold > Player.Instance.PlayerRep)
            StartCoroutine(NotThisContract(_noEnoughtRep));
        else
        {
            _planningPanel.SetActive(true);
            TempGameManager.Instance.PlanningPhaseContractInfoDisplay();
            TempGameManager.Instance.DisplayUnlockedTools();
            TempGameManager.Instance.DisplayUnlockedCrewMembers();
        }
    }

    private IEnumerator NotThisContract(string _string)
    {
        _errorLog.SetActive(true);
        errorTMP.text = _string;
        
        yield return new WaitForSeconds(3f);
        
        _errorLog.SetActive(false);
    }

    private void OnEnable()
    {
        _errorLog.SetActive(false);
    }
}

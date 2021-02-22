using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private bool _isGameRunning;
    public bool IsGameRunning => _isGameRunning;

    private int _anomaly;

    [SerializeField] private int necessaryToolId;

    public static event Action gameOverEvt;

    public static event Action<Tools> toolSwapEvt;
    
    void Awake()
    {
        _isGameRunning = true;
        
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private bool _isGameRunning;
    public bool IsGameRunning => _isGameRunning;

    public static event Action gameOverEvt;

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
}

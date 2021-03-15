using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
         _canvasGroup = GetComponent<CanvasGroup>();
         _canvasGroup.alpha = 0;
    }

    private void OnGameOver()
    {
        _canvasGroup.alpha = 1;
    }
    
    private void OnEnable()
    {
        GameManager.gameOverEvt += OnGameOver;
    }
    
    private void OnDisable()
    {
        GameManager.gameOverEvt -= OnGameOver;
    }
}

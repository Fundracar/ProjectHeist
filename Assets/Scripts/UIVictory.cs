using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIVictory : MonoBehaviour
{
   private CanvasGroup _canvasGroup;

   private void Awake()
   {
      _canvasGroup = GetComponent<CanvasGroup>();
   }

   private void OnVictory()
   {
      _canvasGroup.alpha = 1;
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

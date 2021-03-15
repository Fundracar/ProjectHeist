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
      _canvasGroup.alpha = 0;
      _canvasGroup.interactable = false;
      _canvasGroup.blocksRaycasts = false;
   }

   private void OnVictory()
   {
      _canvasGroup.alpha = 1;
      _canvasGroup.interactable = true;
      _canvasGroup.blocksRaycasts = true;
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

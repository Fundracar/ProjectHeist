using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipedTool : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameTxt;
    [SerializeField] private Image _image;

    private void OnToolSwap(Tools tool)
    {
        _nameTxt.text = tool.name;
        if(tool.Sprite != null)
            _image.sprite = tool.Sprite;
    }

    private void OnEnable()
    {
        GameManager.toolSwapEvt += OnToolSwap;
    }
    
    private void OnDisable()
    {
        GameManager.toolSwapEvt -= OnToolSwap;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : MonoBehaviour
{
    public Slider slider;
    private float currentValue = 0f;
    [SerializeField] private Image _img;

    public float CurrentValue
    {
        set
        {
            currentValue = value;
            slider.value = currentValue;
        }
    }

    public void SwitchSprite(Sprite sprite)
    {
        _img.sprite = sprite;
    }
    
    void Start () {
        CurrentValue = 0f;
    }
}

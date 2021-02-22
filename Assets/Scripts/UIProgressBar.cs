using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : MonoBehaviour
{
    public Slider slider;
    private float currentValue = 0f;

    public float CurrentValue
    {
        set
        {
            currentValue = value;
            slider.value = currentValue;
        }
    }
    
    void Start () {
        CurrentValue = 0f;
    }
}

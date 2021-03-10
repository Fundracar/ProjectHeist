using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnomalyBar : MonoBehaviour
{
    private static UIAnomalyBar _instance;
    public static UIAnomalyBar Instance => _instance;
    
    public Slider slider;
    private float currentValue = 0f;

    public float CurrentValue
    {
        set
        {
            currentValue = value;
            slider.value = currentValue / 100;
        }
    }
    
    void Awake () 
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Debug.LogWarning($"{this.GameManagerStamp()} instance already created");
            Destroy(this);
            return;
        }
        
        slider = GetComponent<Slider>();
        CurrentValue = 0f;
        
    }
}

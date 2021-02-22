using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    protected bool isActive = false;
    
    public void OnInteraction()
    {
        ActiveObject();
        PlayAnim();
    }

    // Play the object Animation
    private void PlayAnim()
    {
        Debug.Log("Active Animation");
    }

    // Active the object effect
    protected virtual void ActiveObject()
    {
        
    }
}

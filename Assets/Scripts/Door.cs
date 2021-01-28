using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractiveObject
{
    protected override void ActiveObject()
    {
        isActive = !isActive;
        
        if(isActive)
            Debug.Log("Door is now open");
        else
            Debug.Log("Door is now close");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCam : InteractiveObject
{
    protected override void ActiveObject()
    {
        GetComponentInChildren<EnemyCam>().enabled = false;
        GetComponentInChildren<Light>().enabled = false;
    }
}

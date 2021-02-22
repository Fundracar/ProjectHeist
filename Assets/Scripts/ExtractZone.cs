using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bag"))
            other.gameObject.SetActive(false);
    }
}

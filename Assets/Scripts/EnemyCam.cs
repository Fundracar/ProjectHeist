using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCam : Enemy
{
    [SerializeField] private bool isRotate;
    [SerializeField] private float rotationAmplitude;

    private void Start()
    {
        if (isRotate)
            StartCoroutine(Rotate());
    }
    
    protected override IEnumerator Rotate()
    {
        while (isRotate)
        {
            
        }
        yield return null;
    }
}

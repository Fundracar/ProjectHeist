using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _detectionPercentage;

    [SerializeField] private EnemySettings _settings;
    

    private void Update()
    {
        throw new NotImplementedException();
    }

    protected virtual void Move()
    {
        
    }

    protected IEnumerator DetectedCharacter()
    {
        yield return null;
    }
}

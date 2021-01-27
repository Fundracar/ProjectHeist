using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _detectionPercentage;

    [SerializeField] protected EnemySettings _settings;

    protected virtual void Move()
    {
        
    }

    protected IEnumerator DetectedCharacter()
    {
        yield return null;
    }
}

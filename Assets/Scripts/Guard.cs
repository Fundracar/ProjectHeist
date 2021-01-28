using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Guard : Enemy
{
    private NavMeshAgent _agent;

    private Vector3 startPoint;
    public Vector3 StartPoint
    {
        set => startPoint = value;
    }

    private int _pathIndex = 0;
    private int _maxIndex;

    private Queue<Transform> _path;
    private List<Transform> _stockedPoints = new List<Transform>();
    //private Transform[] _stockedPoints;

    private bool returnAtSpawn = false;

    public Queue<Transform> Path
    {
        get => _path;
        set => _path = value;
    }

    private Vector3 _dest;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _settings.Speed;
        _agent.updateRotation = false;
        _agent.destination = _path.Peek().position;

        _maxIndex = _path.Count - 1;
        Debug.Log(_path.Peek().position);
    }

    // Assign a new destination
    protected override void Move()
    {
        if (Math.Abs(_agent.remainingDistance) < 0.2f)
        {
            if (_pathIndex < _maxIndex)
            {
                if (!returnAtSpawn)
                {
                    _stockedPoints.Add(_path.Peek());
                    _path.Dequeue();
                    _pathIndex++;
                }

                returnAtSpawn = false;
                _agent.destination = _path.Peek().position;
            }
            else
            {
                _pathIndex = 0;
                _agent.destination = startPoint;
                StartCoroutine(ChangedPath());
                returnAtSpawn = true;
            }
        }
        
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_agent.velocity.normalized)
            , Time.deltaTime * 8);
    }

    IEnumerator ChangedPath()
    {
        int i;
        
        while (_stockedPoints.Count > 0)
        {
             i = Random.Range(0, _stockedPoints.Count);
            _path.Enqueue(_stockedPoints[i]);
            _stockedPoints.RemoveAt(i);
            yield return null;
        }
        _stockedPoints.Clear();
    }
}

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

    enum States
    {
        Patrol = 0,
        Purchase = 1
    }

    private States _states = 0;

    private int _pathIndex = 0;
    private int _maxIndex;

    private Queue<Transform> _path;
    private List<Transform> _stockedPoints = new List<Transform>();
    //private Transform[] _stockedPoints;

    private bool returnAtSpawn = false;

    private Vector3 _lastCharPos;

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

        base.Start();
    }

    IEnumerator ResetState()
    {
        yield return null;
    }
    
    protected override IEnumerator InvestigateAction()
    {
        _states = States.Purchase;
        _lastCharPos = _characterPos;

        while (Math.Abs(_agent.remainingDistance) < 0.2f)
            yield return null;
        
        yield return new WaitForSeconds(_settings.WaitTime * 2);

        _states = States.Patrol;
    }
    
    protected override IEnumerator Move()
    {
        Debug.Log("Start Move!");
        
        while (GameManager.Instance.IsGameRunning)
        {
            Debug.Log("Is Running!");
            
            if (_states == States.Patrol)
            {
                if (Math.Abs(_agent.remainingDistance) < 0.2f)
                {
                    yield return new WaitForSeconds(_settings.WaitTime);
                    
                    if (_pathIndex < _maxIndex)
                    {

                        if (!returnAtSpawn)
                            NextPatrolPoint();
                        
                        returnAtSpawn = false;
                        
                        // Assign a new destination
                        _agent.destination = _path.Peek().position;
                    }
                    else
                        ReturnSpawn();
                }
            }
            else
            {
                // Assign a new destination
                _agent.destination = _lastCharPos;
            }
            
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(_agent.velocity.normalized)
                , Time.deltaTime * 8);

            yield return null;
        }

        Debug.Log("End Move!");
    }

    // Assign the next point 
    void NextPatrolPoint()
    {
        _stockedPoints.Add(_path.Peek());
        _path.Dequeue();
        _pathIndex++;
    }

    // When the Entity finish the patrol return to the start
    void ReturnSpawn()
    {
        _pathIndex = 0;
        returnAtSpawn = true;
        
        // Assign a new destination
        _agent.destination = startPoint;
        
        StartCoroutine(RandomizedPath());
    }

    // Create a new patrol
    private IEnumerator RandomizedPath()
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

    private void OnGameOver()
    {
        _agent.destination = transform.position;
        _agent.speed = 0;
    }
    
    private void OnEnable()
    {
        GameManager.gameOverEvt += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.gameOverEvt -= OnGameOver;
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;
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
        StartCoroutine(Move());
        StartCoroutine(OpenDoor());
    }

    IEnumerator ResetState()
    {
        yield return null;
    }
    
    protected override IEnumerator InvestigateAction()
    {
        _states = States.Chase;
        
        while (_characterInFoV && CalculateAngle() <= _settings.AngleDetection)
        {
            if(FindPlayer())
                _lastCharPos = _characterPos;
            
            yield return null;
        }
    }

    protected override IEnumerator Rotate()
    {
        _states = States.Search;
        
        Vector3 startingPos = transform.rotation.eulerAngles;
        Quaternion checkRight = Quaternion.Euler(0,startingPos.y + _settings.AngleDetection -1, 0);
        Quaternion checkLeft = Quaternion.Euler(0,startingPos.y - _settings.AngleDetection -1 , 0);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / 2;
            transform.rotation = Quaternion.Slerp(transform.rotation, checkLeft, t);
            yield return null;

            if (_characterInFoV && CalculateAngle() <= _settings.AngleDetection/2)
            {
               if (FindPlayer())
               {
                   StartCoroutine(InvestigateAction());
                   yield break; 
               } 
            }
        }

        t = 0;
        while (t < _settings.WaitTime)
        {
            t += Time.deltaTime;
            
            if (_characterInFoV && CalculateAngle() <= _settings.AngleDetection/2)
            {
                if (FindPlayer())
                {
                    StartCoroutine(InvestigateAction());
                    yield break; 
                } 
            }
        }
        
        
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / 2;
            transform.rotation = Quaternion.Slerp(transform.rotation, checkRight, t);
            yield return null;
            
            if (_characterInFoV && CalculateAngle() <= _settings.AngleDetection/2)
            {
                if (FindPlayer())
                {
                    StartCoroutine(InvestigateAction());
                    yield break; 
                } 
            }
        }
        
        yield return new WaitForSeconds(_settings.WaitTime);
        
        _states = States.Patrol;
    }
    
    protected override IEnumerator Move()
    {
        while (GameManager.Instance.IsGameRunning)
        {
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
            else if (_states == States.Chase)
            {
                // Assign a new destination
                _agent.destination = _lastCharPos;
                if (Math.Abs(_agent.remainingDistance) < 0.2f)
                {
                    yield return Rotate();
                }
            }
            
            if(_agent.desiredVelocity.magnitude > 0)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(_agent.velocity.normalized)
                , Time.deltaTime * 8);

            yield return null;
        }
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

    private IEnumerator OpenDoor()
    {
        while (GameManager.Instance.IsGameRunning)
        {
            RaycastHit hit;
             Physics.Raycast(transform.position, transform.forward, out hit, 2f, layerMask);
             Debug.DrawRay(transform.position, transform.forward * 2f, Color.blue);

             if (hit.collider == null)
             {
                 
             }
             else if (hit.collider.CompareTag("Door"))
             {
                 hit.collider.GetComponentInParent<InteractiveObject>().OnGuardInteraction();
             }
                 

             yield return null;
        }

        yield return null;
    }

    private void OnGameOver()
    {
        _agent.destination = transform.position;
        _agent.speed = 0;
    }

    private bool seeDoor = false;

    private void OnEnable()
    {
        GameManager.gameOverEvt += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.gameOverEvt -= OnGameOver;
    }
    
}

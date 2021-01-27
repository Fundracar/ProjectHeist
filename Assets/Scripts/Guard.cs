using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : Enemy
{
    private NavMeshAgent _agent;


    private List<Transform> _path;
    public List<Transform> Path
    {
        set => _path = value;
    }

    private Transform _dest;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _settings.Speed;
        _agent.updateRotation = false;
        _agent.destination = _path[0].transform.position;
    }

    private void Update()
    {
        NextDestination();
        
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_agent.velocity.normalized)
            , Time.deltaTime * 8);
    }

    // Assign a new destination
    private void NextDestination()
    {
        if (Math.Abs(_agent.remainingDistance) < 0.2f)
        {
            _path.Add(_path[0]);
            _path.RemoveAt(0);
            _agent.destination = _path[0].transform.position;
        }
    }
}

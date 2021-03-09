using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractZone : InteractiveObject
{
    private CharacterController _cc;
    
    private void Start()
    {
        _cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bag"))
            OnBag(other.gameObject);
    }

    protected override void ActiveObject()
    {
        if(GameManager.Instance.MainGoalCompleted)
            GameManager.Instance.OnVictory();
        else if (_cc.EquippedBag != null)
            _cc.DropBag();
    }

    private void OnBag(GameObject bag)
    {
        bag.SetActive(false);
        if (bag.GetComponent<Bag>().IsGoal)
            GameManager.Instance.MainGoalCompleted = true;
    }
}

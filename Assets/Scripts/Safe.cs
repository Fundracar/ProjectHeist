using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Safe : InteractiveObject
{
    [Header("Safe")]
    [SerializeField] private GameObject _bagGold;
    [SerializeField] private GameObject _bagJewel;
    [SerializeField] private GameObject _bagMoney;

    [SerializeField] private Transform _bagSpawnPoint;

    private GameObject bag;
    [SerializeField] private bool _containGoal;
    
    private enum eContain
    {
        Gold,
        Jewel,
        Money
    }
    [SerializeField] private eContain _contain;
    
    private void Start()
    {
        if (_contain == eContain.Gold)
            InstantiateBag(_bagGold);
        else if (_contain == eContain.Jewel)
            InstantiateBag(_bagJewel);
        else
            InstantiateBag(_bagMoney);
    }

    //Spawn a bag with the same content than him
    private void InstantiateBag(GameObject iBag)
    {
        bag = Instantiate(iBag, _bagSpawnPoint.position,   Quaternion.identity);
        bag.SetActive(false);
        bag.GetComponent<Bag>().IsGoal = _containGoal;
    }
    
    protected override void ActiveObject()
    {
        bag.SetActive(true);
        GetComponent<Collider>().enabled = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Safe : InteractiveObject
{
    [SerializeField] private GameObject _bagGold;
    [SerializeField] private GameObject _bagJewel;
    [SerializeField] private GameObject _bagMoney;

    private GameObject bag;
    
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
        bag = Instantiate(iBag, transform.position + new Vector3(0,-1f,0), Quaternion.identity);
        bag.SetActive(false);
    }
    
    protected override void ActiveObject()
    {
        bag.SetActive(true);
        GetComponent<Collider>().enabled = false;
    }
}

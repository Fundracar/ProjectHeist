using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : InteractiveObject
{
    private Transform _tr;
    private Collider _collider;

    public Collider Collider => _collider;

    private CharacterController _cc;
    private Transform _ccTr;

    private enum eContain
    {
        Gold,
        Jewel,
        Money
    }
    [SerializeField] private eContain _contain;

    private void Awake()
    {
        _tr = GetComponent<Transform>();
        _collider = GetComponent<Collider>();
        
        _cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        _ccTr = _cc.GetComponent<Transform>();
    }

    protected override void ActiveObject()
    {
        transform.parent = _ccTr;
        _tr.localPosition = new Vector3(-1,0,0);
        _tr.localRotation = Quaternion.Euler(0,0,0);
        _cc.EquippedBag = this;

        _collider.enabled = false;
    }
}

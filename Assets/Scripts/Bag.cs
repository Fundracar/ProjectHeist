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

    [SerializeField] private bool isGoal;
    public bool IsGoal
    {
        get => isGoal;
        set => isGoal = value;
    }

    [SerializeField] private BagData _data;

    private void Awake()
    {
        _tr = GetComponent<Transform>();
        _collider = GetComponent<Collider>();
        
        _cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        _ccTr = _cc.GetComponent<Transform>();
    }

    protected override void ActiveObject()
    {
        if (_cc.EquippedBag == null)
        {
             transform.parent = _ccTr;
             _tr.localPosition = new Vector3(-1,0,0);
             _tr.localRotation = Quaternion.Euler(0,0,0);
             _cc.EquippedBag = this;
            
             _collider.enabled = false;
        }
    }
}

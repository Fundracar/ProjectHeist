using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public abstract class InteractiveObject : MonoBehaviour
{
    [SerializeField] protected bool isActive = false;
    
    [SerializeField]protected bool needTool;
    public bool NeedTool
    {
        get => needTool;
        set => needTool = value;
    }

    [SerializeField] private Tools _firstTool;
    private int _firstToolId;
    public int FirstToolId => _firstToolId;
    
    [SerializeField] private bool _haveSecondTool;
    public bool HaveSecondTool => _haveSecondTool;
    
    [SerializeField]private Tools _secondTool;
    private int _secondToolId;
    public int SecondToolId => _secondToolId;

    private void Awake()
    {
        if(needTool)
            _firstToolId = _firstTool.ToolsId;
        
        if(_haveSecondTool)
            _secondToolId = _secondTool.ToolsId;
    }

    public void OnInteraction( )
    {
        ActiveObject();
        PlayAnim();
    }

    // Play the object Animation
    private void PlayAnim()
    {
        Debug.Log("Active Animation");
    }

    // Active the object effect
    protected abstract void ActiveObject();
}

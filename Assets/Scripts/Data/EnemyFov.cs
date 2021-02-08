using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovSettings : ScriptableObject
{
    private int _precision = 20;
    public int Precision => _precision;

    private Material _material;
    public Material Material => _material;

    private bool _debug = false;
    public bool Debug => _debug;

    private float _freq = 0.05F;
    public float Freq => _freq;

    private LayerMask _mask;
    public LayerMask Mask => _mask;
}

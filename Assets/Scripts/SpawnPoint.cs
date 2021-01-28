using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private SpawnPointData _data = default;
    [SerializeField] private List<Transform> _pathPoint;

    void Awake()
    {
        GetComponent<Renderer>().enabled = false;
        
        foreach (var point in _pathPoint)
        {
            point.GetComponent<Renderer>().enabled = false;
        }

        GameObject g = Instantiate(_data.GuardPrefab, transform.position, Quaternion.identity);
        Guard guard = g.GetComponent<Guard>();

        guard.Path = new Queue<Transform>(_pathPoint.Count);
            
        foreach (var point in _pathPoint)
        {
            guard.Path.Enqueue(point);
        }
        
        g.GetComponent<Guard>().StartPoint = transform.position;
    }
    
    // Create a new PathPoint
    public void AddPoint()
    {
        GameObject p = Instantiate(_data.Point,gameObject.transform);
        p.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        _pathPoint.Add(p.transform);
    }

    // Remove the last created PathPoint
    public void RemovePoint()
    {
        DestroyImmediate(_pathPoint[_pathPoint.Count - 1].gameObject);
        _pathPoint.RemoveAt(_pathPoint.Count - 1);
    }

    // Change the color for the SpawnPoint ant all the PathPoint 
    public void ChangeColors(Color matColor)
    {
        Renderer rend = GetComponent<Renderer>();

        if (rend != null)
            rend.sharedMaterial.color = matColor;
        
        foreach (var t in _pathPoint)
        {
            rend = t.gameObject.GetComponent<Renderer>();

            if (rend != null)
                rend.sharedMaterial.color = matColor;
        }
    }
}

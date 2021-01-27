using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnPoint))]
public class SpawnPointEditor : Editor
{
    Color matColor = Color.white;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        GUILayout.Space(20);
        
        SpawnPoint spawnPoint = (SpawnPoint) target;
        
        if (GUILayout.Button("Add Point"))
            spawnPoint.AddPoint();
        
        if (GUILayout.Button("Remove Point"))
            spawnPoint.RemovePoint();

        if (GUI.changed)
            EditorUtility.SetDirty(target);

        GUILayout.Space(20);
        
        matColor = EditorGUILayout.ColorField("New Color", matColor);

        if (GUILayout.Button("Change Color!"))
            spawnPoint.ChangeColors(matColor);
    }
}

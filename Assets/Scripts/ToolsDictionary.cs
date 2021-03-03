using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsDictionary : MonoBehaviour
{
    private static ToolsDictionary _instance;
    public static ToolsDictionary Instance()
    {
        if (_instance == null)
            _instance = new ToolsDictionary();
        else
            Debug.Log("Already have a reference");

        return _instance;
    }

    public static Dictionary<int,Tools> InitializeDict(List<Tools> toolsList) 
    {
        Dictionary<int, Tools> toolsDict = new Dictionary<int, Tools>();
        
        foreach (Tools availableTool in toolsList)
        {
            Tools t = availableTool;
            toolsDict.Add(t.ToolsId, t);
            Debug.Log("+1 tool");
        }

        return toolsDict;
    }
}

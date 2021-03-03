using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractDict : MonoBehaviour
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

    public static Dictionary<int,Contract> InitializeDict(List<Contract> toolsList) 
    {
        Dictionary<int, Contract> toolsDict = new Dictionary<int, Contract>();
        
        foreach (Contract availableTool in toolsList)
        {
            Contract c = availableTool;
            toolsDict.Add(c.contractId, c);
            Debug.Log("+1 tool");
        }

        return toolsDict;
    }
}

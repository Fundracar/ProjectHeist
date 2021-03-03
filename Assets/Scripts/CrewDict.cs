using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewDict : MonoBehaviour
{
    private static CrewDict _instance;
    public static CrewDict Instance()
    {
        if (_instance == null)
            _instance = new CrewDict();
        else
            Debug.Log("Already have a reference");

        return _instance;
    }
    
    public static Dictionary<int,Crew> InitializeDict(List<Crew> crewList) 
    {
        Dictionary<int, Crew> crewDict = new Dictionary<int, Crew>();
        
        foreach (Crew availableTool in crewList)
        {
            Crew t = availableTool;
            crewDict.Add(t.crewId, t);
            Debug.Log("+1 tool");
        }

        return crewDict;
    }
}

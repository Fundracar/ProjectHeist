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
    
    public static Dictionary<int,GameObject> InitializeDict(List<GameObject> crewList) 
    {
        Dictionary<int, GameObject> crewDict = new Dictionary<int, GameObject>();
        
        foreach (GameObject availableCrew in crewList)
        {
            GameObject t = availableCrew;
            crewDict.Add(availableCrew.GetComponent<Crew>().CrewId, t);
            Debug.Log("+1 tool");
        }

        return crewDict;
    }
}

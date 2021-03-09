using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    private const string _monoStampCol = "5200ff";
    private const string _saveStampCol = "00ff00";
    private const string _gameManagerStampCol = "ff00000";
        
    private static string BuildStamp(MonoBehaviour obj, string color)
    {
        return $"<color=#{color}><b>{obj.GetType()}</b></color> |";
    }

    public static string GetStamp(this MonoBehaviour obj)
    {
        return BuildStamp(obj, _monoStampCol);
    }
    
    public static string SaveStamp(this MonoBehaviour obj)
    {
        return BuildStamp(obj, _saveStampCol);
    }
    
    public static string GameManagerStamp(this MonoBehaviour obj)
    {
        return BuildStamp(obj, _gameManagerStampCol);
    }
}

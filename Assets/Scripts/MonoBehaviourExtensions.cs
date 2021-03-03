using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    private const string _monoStampCol = "5200ff";
        
    private static string BuildStamp(MonoBehaviour obj, string color)
    {
        return $"<color=#{color}><b>{obj.GetType()}</b></color> |";
    }

    public static string GetStamp(this MonoBehaviour obj, string color)
    {
        return BuildStamp(obj, color);
    }
}

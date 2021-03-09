using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewAction : Crew
{
    enum eTarget
    {
        Door,
        Cam,
        Guard
    }

    [SerializeField] private eTarget _target;
    
    public override void ActiveIt()
    {
        int i = 0;
        
        Debug.Log($"{this.GetStamp()} Start Action");
        switch (_target)
        {
            case eTarget.Cam:
                i = Random.Range(0, GameManager.Instance.EnemyCams.Count);
                GameManager.Instance.EnemyCams[i].SetActiveFalse();
                Debug.Log($"{this.GetStamp()} Disable a Cam (i :" + GameManager.Instance.EnemyCams[i] + ")");
                break;
            case eTarget.Guard:
                i = Random.Range(0, GameManager.Instance.EnemyCams.Count);
                GameManager.Instance.Guards[Random.Range(0, GameManager.Instance.Guards.Count)].enabled = false;
                Debug.Log($"{this.GetStamp()} Disable a Guard (i : " + i +")");
                break;
            default:
                i = Random.Range(0, GameManager.Instance.EnemyCams.Count);
                GameManager.Instance.Doors[Random.Range(0, GameManager.Instance.Doors.Count)].NeedTool = false;
                Debug.Log($"{this.GetStamp()} Disable a Door (i : " + i +")");
                break;
        }
        
        GameManager.Instance.UpAnomaly(AnomalyCost);
    }
    
}

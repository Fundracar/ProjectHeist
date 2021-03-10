using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewBonus : Crew
{
    public enum eActiveAt
    {
        Start,
        End
    }
    
    [SerializeField] private eActiveAt _activeAt = default;
    public eActiveAt ActiveAt => _activeAt;
    
    public enum eTarget
    {
        Reward,
        Rep
    }
    
    [SerializeField] private eTarget _target;
    public eTarget Target => _target;
    
    [SerializeField] private int _bonusValue;
    public int BonusValue => _bonusValue;
    
    public override void ActiveIt()
    {
        switch (_target)
        {
            case eTarget.Reward:
                GameManager.Instance.MoneyBonus = _bonusValue;
                Debug.Log($"{this.GetStamp()} Bonus apply : " + _bonusValue);
                break;
            default:
                Debug.Log($"{this.GetStamp()} Not normal");
                break;
        }
    }
}

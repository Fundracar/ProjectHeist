using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tools", menuName = "Heist/item/Bag", order = 0)]
public class BagData : ScriptableObject
{
    private enum eContain
    {
        Gold,
        Jewel,
        Money
    }
    [SerializeField] private eContain _contain;

    [SerializeField] private int _moneyValue;
    public int MoneyValue => _moneyValue;
}

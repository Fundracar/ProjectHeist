using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Crew", menuName = "Heist/item/Crew", order = 0)]
public class Crew : MonoBehaviour
{
    public string crewName;
    public int crewId;
    public int crewReputationTreshold;
    public Image crewSprite;
}

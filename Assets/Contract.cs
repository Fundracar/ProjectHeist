using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contract : MonoBehaviour
{
    public string contractName;
    public int contractId;
    public int vigilanceScore;
    public int reputationBaseReward;
    public int moneyBaseReward;
    public int reputationTreshold;
    
    // Start is called before the first frame update
    void Start()
    {
        //GameObject tempGameManager = GameObject.FindWithTag("TempGameManager");
        //Debug.Log("1 ca marche");
       // TempGameManager tempGameManagerComponent = tempGameManager.GetComponent<TempGameManager>();
        //Debug.Log("2 ca marche");
        //tempGameManagerComponent.dictOfContracts.Add(this.contractId, this);
        //Debug.Log("3 ca marche");
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

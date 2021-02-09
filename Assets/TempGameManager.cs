using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGameManager : MonoBehaviour
{
    public Dictionary<int, Contract> dictOfContracts;
    public List<GameObject> listOfContractsPrefab;
    public Contract storedContract;
    public int currentPlayerRep;
    public int playerRep;



    public Contract GetContractInDict(int linkedContractId)
    {
        Contract foundContract = dictOfContracts[linkedContractId];
        return foundContract;

    }

    //Method to update the player reputation when going back to MainMenuScene from saved reward (int) if successfull heist
    public void RaisePlayerRep()
    {
        currentPlayerRep = currentPlayerRep + playerRep;
    }


    // Start is called before the first frame update
    void Start()
    {
        dictOfContracts = new Dictionary<int, Contract>();

        foreach (GameObject currentContractObject in listOfContractsPrefab)
        {
            Contract currentContractComponent = currentContractObject.GetComponent<Contract>();
            dictOfContracts.Add(currentContractComponent.contractId, currentContractComponent);
            Debug.Log("+1ContractInDict");

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

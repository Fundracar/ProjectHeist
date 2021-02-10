using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempGameManager : MonoBehaviour
{
    public Dictionary<int, Contract> dictOfContracts;
    public List<GameObject> listOfContractsPrefab;
    public Contract storedContract;
    public GameObject infoBoxPrefab;
    public Vector3 buttonPosition;
    public Vector3 infoBoxOffSet;
    public int currentPlayerRep;
    public int playerRep;


    //Method to destroy the info box clone after hovering
    public void destroyInfoBox()
    {
        GameObject instantiatedInfoBox = GameObject.Find("Contract-InfoBox(Clone)");
        if (instantiatedInfoBox)
        {
            Destroy(instantiatedInfoBox);
        }
    }

    //Method to update the player reputation when going back to MainMenuScene from saved reward (int) if successfull heist
    public void RaisePlayerRep()
    {
        currentPlayerRep = currentPlayerRep + playerRep;
    }


    //Method used to display the contract informations on the InfoBox after hovering the button
    public void InfoBoxDisplay(int linkedContractId, GameObject contractButton)
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        Contract foundContract = dictOfContracts[linkedContractId];

        buttonPosition = contractButton.transform.position;
        float infoBoxOffSetX = contractButton.transform.position.x + 260;
        float infoBoxOffSetY = contractButton.transform.position.y;
        float infoBoxOffSetZ = contractButton.transform.position.z;
        infoBoxOffSet = new Vector3 (infoBoxOffSetX, infoBoxOffSetY, infoBoxOffSetZ);

        GameObject infoBoxPrefabClone = Instantiate(infoBoxPrefab, infoBoxOffSet, Quaternion.identity, canvas.transform);

        GameObject infoBoxContractName = GameObject.FindGameObjectWithTag("ContractName");
        GameObject infoBoxVigilanceScore = GameObject.FindGameObjectWithTag("VigilanceScore");
        GameObject infoBoxMoneyBaseReward = GameObject.FindGameObjectWithTag("MoneyBaseRewardValue");
        GameObject infoBoxReputationBaseReward = GameObject.FindGameObjectWithTag("ReputationBaseRewardValue");

        Text textToChangeContractName = infoBoxContractName.GetComponent<Text>();
        textToChangeContractName.text = foundContract.contractName;
        Text textToChangeVigilanceScore = infoBoxVigilanceScore.GetComponent<Text>();
        textToChangeVigilanceScore.text = foundContract.vigilanceScore.ToString();
        Text textToChangeMoneyBaseReward = infoBoxMoneyBaseReward.GetComponent<Text>();
        textToChangeMoneyBaseReward.text = foundContract.moneyBaseReward.ToString();
        Text textToChangeReputationBaseReward = infoBoxReputationBaseReward.GetComponent<Text>();
        textToChangeReputationBaseReward.text = foundContract.reputationBaseReward.ToString();
 
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

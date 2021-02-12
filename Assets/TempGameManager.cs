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
        infoBoxOffSet = buttonPosition + new Vector3(260, 0, 0);
        GameObject infoBoxPrefabClone = Instantiate(infoBoxPrefab, infoBoxOffSet, Quaternion.identity, canvas.transform);

        Text infoBoxContractName = GameObject.FindGameObjectWithTag("ContractName").GetComponent<Text>();
        infoBoxContractName.text = foundContract.contractName;
        Text infoBoxVigilanceScore = GameObject.FindGameObjectWithTag("VigilanceScore").GetComponent<Text>();
        infoBoxVigilanceScore.text = foundContract.vigilanceScore.ToString();
        Text infoBoxMoneyBaseReward = GameObject.FindGameObjectWithTag("MoneyBaseRewardValue").GetComponent<Text>();
        infoBoxMoneyBaseReward.text = foundContract.moneyBaseReward.ToString();
        Text infoBoxReputationBaseReward = GameObject.FindGameObjectWithTag("ReputationBaseRewardValue").GetComponent<Text>();
        infoBoxReputationBaseReward.text = foundContract.reputationBaseReward.ToString();

    }


    public void ContractAllInfoDisplay(int linkedContractId)
    {
        Contract foundContract = dictOfContracts[linkedContractId];

        Text allInfoDisplayContractName = GameObject.FindGameObjectWithTag("AllDisplay.ContractName").GetComponent<Text>();
        allInfoDisplayContractName.text = foundContract.contractName;
        Text allInfoDisplayContractDescription = GameObject.FindGameObjectWithTag("AllDisplay.ContractDescription").GetComponent<Text>();
        allInfoDisplayContractDescription.text = foundContract.contractDescription;
        Text allInfoDisplayContractMainObjective = GameObject.FindGameObjectWithTag("AllDisplay.ContractMainObjective").GetComponent<Text>();
        allInfoDisplayContractMainObjective.text = foundContract.contractMainObjective;
        Text allInfoDisplayContractConstraints = GameObject.FindGameObjectWithTag("AllDisplay.ContractConstraints").GetComponent<Text>();
        allInfoDisplayContractConstraints.text = foundContract.contractConstraints;
        Text allInfoDisplayContractBonusObjective = GameObject.FindGameObjectWithTag("AllDisplay.ContractBonusObjective").GetComponent<Text>();
        allInfoDisplayContractBonusObjective.text = foundContract.contractBonusObjective;
        Text allInfoDisplayContractBaseFundReward = GameObject.FindGameObjectWithTag("AllDisplay.ContractBaseFundReward").GetComponent<Text>();
        allInfoDisplayContractBaseFundReward.text = foundContract.moneyBaseReward.ToString();
        Text allInfoDisplayContractBaseReputationReward = GameObject.FindGameObjectWithTag("AllDisplay.ContractBaseReputationReward").GetComponent<Text>();
        allInfoDisplayContractBaseReputationReward.text = foundContract.reputationBaseReward.ToString();
        Text allInfoDisplayContractBonusFundReward = GameObject.FindGameObjectWithTag("AllDisplay.ContractBonusFundReward").GetComponent<Text>();
        allInfoDisplayContractBonusFundReward.text = foundContract.moneyBonusReward.ToString();
        Text allInfoDisplayContractBonusReputationReward = GameObject.FindGameObjectWithTag("AllDisplay.ContractBonusReputationReward").GetComponent<Text>();
        allInfoDisplayContractBonusReputationReward.text = foundContract.reputationBonusReward.ToString();
        Text allInfoDisplayContractReputationTreshold = GameObject.FindGameObjectWithTag("AllDisplay.ContractReputationTreshold").GetComponent<Text>();
        allInfoDisplayContractReputationTreshold.text = foundContract.reputationTreshold.ToString();

        storedContract = foundContract; //Current selected contract to be used to display the right contract in the planning Menu

    }

    // Start is called before the first frame update
    void Start()
    {
        dictOfContracts = new Dictionary<int, Contract>();

        foreach (GameObject currentContractObject in listOfContractsPrefab)
        {
            Contract currentContractComponent = currentContractObject.GetComponent<Contract>();
            dictOfContracts.Add(currentContractComponent.contractId, currentContractComponent);
            //Debug.Log("+1ContractInDict");

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

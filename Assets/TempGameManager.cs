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


    public void ContractAllInfoDisplay(int linkedContractId)
    {
        Contract foundContract = dictOfContracts[linkedContractId];

        GameObject allInfoDisplayContractName = GameObject.FindGameObjectWithTag("AllDisplay.ContractName");
        GameObject allInfoDisplayContractDescription = GameObject.FindGameObjectWithTag("AllDisplay.ContractDescription");
        GameObject allInfoDisplayContractMainObjective = GameObject.FindGameObjectWithTag("AllDisplay.ContractMainObjective");
        GameObject allInfoDisplayContractConstraints = GameObject.FindGameObjectWithTag("AllDisplay.ContractConstraints");
        GameObject allInfoDisplayContractBonusObjective = GameObject.FindGameObjectWithTag("AllDisplay.ContractBonusObjective");
        GameObject allInfoDisplayContractBaseFundReward = GameObject.FindGameObjectWithTag("AllDisplay.ContractBaseFundReward");
        GameObject allInfoDisplayContractBaseReputationReward = GameObject.FindGameObjectWithTag("AllDisplay.ContractBaseReputationReward");
        GameObject allInfoDisplayContractBonusFundReward = GameObject.FindGameObjectWithTag("AllDisplay.ContractBonusFundReward");
        GameObject allInfoDisplayContractBonusReputationReward = GameObject.FindGameObjectWithTag("AllDisplay.ContractBonusReputationReward");
        GameObject allInfoDisplayContractReputationTreshold = GameObject.FindGameObjectWithTag("AllDisplay.ContractReputationTreshold");
        
        Text allDisplayToChangeContractName = allInfoDisplayContractName.GetComponent<Text>();
        allDisplayToChangeContractName.text = foundContract.contractName;
        Text allDisplayToChangeContractDescription = allInfoDisplayContractDescription.GetComponent<Text>();
        allDisplayToChangeContractDescription.text = foundContract.contractDescription;
        Text allDisplayToChangeContractMainObjective = allInfoDisplayContractMainObjective.GetComponent<Text>();
        allDisplayToChangeContractMainObjective.text = foundContract.contractMainObjective;
        Text allDisplayToChangeContractConstraints = allInfoDisplayContractConstraints.GetComponent<Text>();
        allDisplayToChangeContractConstraints.text = foundContract.contractConstraints;
        Text allDisplayToChangeContractBonusObjective = allInfoDisplayContractBonusObjective.GetComponent<Text>();
        allDisplayToChangeContractBonusObjective.text = foundContract.contractBonusObjective;
        Text allDisplayToChangeContractBaseFundReward = allInfoDisplayContractBaseFundReward.GetComponent<Text>();
        allDisplayToChangeContractBaseFundReward.text = foundContract.moneyBaseReward.ToString();
        Text allDisplayToChangeContractBaseReputationReward = allInfoDisplayContractBaseReputationReward.GetComponent<Text>();
        allDisplayToChangeContractBaseReputationReward.text = foundContract.reputationBaseReward.ToString();
        Text allDisplayToChangeContractBonusFundReward = allInfoDisplayContractBonusFundReward.GetComponent<Text>();
        allDisplayToChangeContractBonusFundReward.text = foundContract.moneyBonusReward.ToString();
        Text allDisplayToChangeContractBonusReputationReward = allInfoDisplayContractBonusReputationReward.GetComponent<Text>();
        allDisplayToChangeContractBonusReputationReward.text = foundContract.reputationBonusReward.ToString();
        Text allDisplayToChangeContractReputationTreshold = allInfoDisplayContractReputationTreshold.GetComponent<Text>();
        allDisplayToChangeContractReputationTreshold.text = foundContract.reputationTreshold.ToString();

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

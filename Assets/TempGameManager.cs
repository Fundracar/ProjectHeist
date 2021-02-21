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
    public List<GameObject> listOfToolsPrefab;
    public Dictionary<int, Tools> dictOfAllTools;
    public Dictionary<int, Tools> dictOfUnlockedTools;
    public List<GameObject> listOfToolButtonPrefab;
    public List<GameObject> listOfCrewMemberPrefab;
    public Dictionary<int, Crew> dictOfAllCrewMembers;
    public Dictionary<int, Crew> dictOfUnlockedCrewMembers;
    public List<GameObject> listOfCrewMemberButton;
    public Tools currentSelectedTool;
    public Crew currentSelectedCrew;

    //Method to Display all unlocked Crew Members
    public void DisplayUnlockedCrewMembers()
    {
        GameObject.FindGameObjectWithTag("CB1").SetActive(false);
        GameObject.FindGameObjectWithTag("CB2").SetActive(false);
        GameObject.FindGameObjectWithTag("CB3").SetActive(false);
        GameObject.FindGameObjectWithTag("CB4").SetActive(false);
        GameObject.FindGameObjectWithTag("CB5").SetActive(false);
        GameObject.FindGameObjectWithTag("CB6").SetActive(false);
        
        int i = 0;
        foreach (KeyValuePair<int, Crew> unlockedCrewMemberIterator in dictOfUnlockedCrewMembers)
        {
            listOfCrewMemberButton[i].SetActive(true);
            //Debug.Log(listOfCrewMemberButton[i].activeSelf);
            Image img = listOfCrewMemberButton[i].GetComponent<Image>();
            img.sprite = unlockedCrewMemberIterator.Value.crewSprite.sprite;
            img.color = unlockedCrewMemberIterator.Value.crewSprite.color;
            i += 1;
        }
    }

    //Method to get all current unlocked CrewMembers
    public void ResolveUnlockedCrewMembers()
    {
        foreach (KeyValuePair<int, Crew> crewMemberIterator in dictOfAllCrewMembers)
        {
            if (crewMemberIterator.Value.crewReputationTreshold <= currentPlayerRep)
            {
                dictOfUnlockedCrewMembers.Add(crewMemberIterator.Key, crewMemberIterator.Value);
                Debug.Log("+1 crewMember unlocked");
            }
        }
    }

    //Method to Display all unlocked Tools
    public void DisplayUnlockedTools()
    {
        GameObject.FindGameObjectWithTag("TB1").SetActive(false);
        GameObject.FindGameObjectWithTag("TB2").SetActive(false);
        GameObject.FindGameObjectWithTag("TB3").SetActive(false);
        GameObject.FindGameObjectWithTag("TB4").SetActive(false);
        GameObject.FindGameObjectWithTag("TB5").SetActive(false);
        GameObject.FindGameObjectWithTag("TB6").SetActive(false);

        int i = 0;
        foreach (KeyValuePair<int, Tools> unlockedToolIterator in dictOfUnlockedTools)
        {
            listOfToolButtonPrefab[i].SetActive(true);
            Debug.Log(listOfToolButtonPrefab[i].activeSelf);
            Image img = listOfToolButtonPrefab[i].GetComponent<Image>();
            img.sprite = unlockedToolIterator.Value.toolSprite.sprite;
            img.color = unlockedToolIterator.Value.toolSprite.color;
            i += 1;
        }
    }


    //Method to get all current unlocked Tools
    public void ResolveUnlockedTools()
    {
        foreach (KeyValuePair<int, Tools> toolIterator in dictOfAllTools)
        { 
            if (toolIterator.Value.toolReputationTreshold <= currentPlayerRep)
            {
                dictOfUnlockedTools.Add(toolIterator.Key, toolIterator.Value);
                Debug.Log("+1 tool unlocked");
            }
                           
        }
        
    }


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

    //Method to display all the contract informations on the side and bottom description bar in the selectionContract Panel
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

    //Method to display the needed contract informations in the planning phase
    public void PlanningPhaseContractInfoDisplay()
    {
        
        Contract selectedContract = storedContract;

        Text planningDisplayContractName = GameObject.FindGameObjectWithTag("PlanningPhase.contractName").GetComponent<Text>();
        planningDisplayContractName.text = selectedContract.contractName;
        Text planningDisplayContractVigilanceScore = GameObject.FindGameObjectWithTag("PlanningPhase.contractVigilanceScoreValue").GetComponent<Text>();
        planningDisplayContractVigilanceScore.text = selectedContract.vigilanceScore.ToString();
        Text planningDisplayContractMainObjective = GameObject.FindGameObjectWithTag("PlanningPhase.contractMainObjective").GetComponent<Text>();
        planningDisplayContractMainObjective.text = selectedContract.contractMainObjective;
        Text planningDisplayContractConstraints = GameObject.FindGameObjectWithTag("PlanningPhase.contractConstraints").GetComponent<Text>();
        planningDisplayContractConstraints.text = selectedContract.contractConstraints;
        Text planningDisplayContractBonusObjective = GameObject.FindGameObjectWithTag("PlanningPhase.contractBonusObjective").GetComponent<Text>();
        planningDisplayContractBonusObjective.text = selectedContract.contractBonusObjective;

    }




    // Start is called before the first frame update
    void Start()
    {
        dictOfContracts = new Dictionary<int, Contract>();
        dictOfAllTools = new Dictionary<int, Tools>();
        dictOfUnlockedTools = new Dictionary<int, Tools>();
        dictOfAllCrewMembers = new Dictionary<int, Crew>();
        dictOfUnlockedCrewMembers = new Dictionary<int, Crew>();

        foreach (GameObject currentContractObject in listOfContractsPrefab)
        {
            Contract currentContractComponent = currentContractObject.GetComponent<Contract>();
            dictOfContracts.Add(currentContractComponent.contractId, currentContractComponent);
            //Debug.Log("+1ContractInDict");
        }

        foreach (GameObject availableTool in listOfToolsPrefab)
        {
            Tools t = availableTool.GetComponent<Tools>();
            dictOfAllTools.Add(t.toolId, t);
            Debug.Log("+1 tool");
        }

        foreach (GameObject availableCrewMember in listOfCrewMemberPrefab)
        {
            Crew c = availableCrewMember.GetComponent<Crew>();
            dictOfAllCrewMembers.Add(c.crewId, c);
            Debug.Log("+1 Crew Member");
        }

        //Add save informations before RaisePlayerRep and ResolveUnlockedTools
        //LoadSaveInfo();
        RaisePlayerRep();
        ResolveUnlockedTools();
        //DisplayUnlockedTools();
        ResolveUnlockedCrewMembers();
        //DisplayUnlockedCrewMembers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

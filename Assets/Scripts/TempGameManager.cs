using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempGameManager : MonoBehaviour
{
    private static TempGameManager _instance;
    public static TempGameManager Instance => _instance;
    
    [Header("Contract")]
    public Dictionary<int, Contract> dictOfContracts;
    public List<GameObject> listOfContractsPrefab;
    public Contract storedContract;
    
    [Header("Info")]
    [SerializeField] private GameObject infoBoxPrefab;
    public Vector3 buttonPosition;
    [SerializeField] private Vector3 infoBoxOffSet;
    [SerializeField] private GameObject hideoutInfoBoxPrefab;
    public Vector3 hideoutToolButtonPosition;
    [SerializeField] private Vector3 hideoutInfoBoxOffSet;
    [SerializeField] private GameObject hideoutCrewInfoBoxPrefab;
    public Vector3 hideoutCrewButtonPosition;
    [SerializeField] private Vector3 hideoutCrewInfoBoxOffSet;

    
    [Header("Tools")]
    [SerializeField] private List<Tools> listOfToolsPrefab;
    public Dictionary<int, Tools> dictOfAllTools;
    public Dictionary<int, Tools> dictOfUnlockedTools;
    public List<GameObject> listOfToolButtonPrefab;
    public Tools currentSelectedTool;
    
    [Header ("Crew")]
    public List<GameObject> listOfCrewMemberPrefab;
    public Dictionary<int, GameObject> dictOfAllCrewMembers;
    public Dictionary<int, GameObject> dictOfUnlockedCrewMembers;
    public List<GameObject> listOfCrewMemberButton;
    public Crew currentSelectedCrew;

    #region Crew
    //Method to Display all unlocked Crew Members
    public void DisplayUnlockedCrewMembers()
    {
        foreach (var button in listOfCrewMemberButton)
            button.SetActive(false);
        
            

        int i = 0;
        foreach (KeyValuePair<int, GameObject> unlockedCrewMemberIterator in dictOfUnlockedCrewMembers)
        {
            listOfCrewMemberButton[i].SetActive(true);
            //Debug.Log(listOfCrewMemberButton[i].activeSelf);
            Image img = listOfCrewMemberButton[i].GetComponent<Image>();
            img.sprite = unlockedCrewMemberIterator.Value.GetComponent<Crew>().CrewSprite;
            listOfCrewMemberButton[i].GetComponent<OnClickButton>().selectedCrew = unlockedCrewMemberIterator.Value;
            //img.color = unlockedCrewMemberIterator.Value.crewSprite.color;
            i++;
        }
    }

    //Method to get all current unlocked CrewMembers
    private void ResolveUnlockedCrewMembers()
    {
        foreach (KeyValuePair<int, GameObject> crewMemberIterator in dictOfAllCrewMembers)
        {
            if (crewMemberIterator.Value.GetComponent<Crew>().CrewReputationTreshold <= Player.Instance.PlayerRep)
            {
                dictOfUnlockedCrewMembers.Add(crewMemberIterator.Key, crewMemberIterator.Value);
                Debug.Log($"{this.GameManagerStamp()}+1 crewMember unlocked", this);
            }
        }
    }
    #endregion

    #region Tools
    //Method to Display all unlocked Tools
    public void DisplayUnlockedTools()
    {
        foreach (var button in listOfToolButtonPrefab)
            button.SetActive(false);
        
        int i = 0;
        foreach (KeyValuePair<int, Tools> unlockedToolIterator in dictOfUnlockedTools)
        {
            listOfToolButtonPrefab[i].SetActive(true);
            Debug.Log(listOfToolButtonPrefab[i].activeSelf);
            Image img = listOfToolButtonPrefab[i].GetComponent<Image>();
            img.sprite = unlockedToolIterator.Value.UnlockedSprite;
            listOfToolButtonPrefab[i].GetComponent<OnClickButton>().selectedTool = unlockedToolIterator.Value;
           //img.color = unlockedToolIterator.Value.Sprite.color;
            i += 1;
        }
    }


    //Method to get all current unlocked Tools
    public void ResolveUnlockedTools()
    {
        foreach (KeyValuePair<int, Tools> toolIterator in dictOfAllTools)
        { 
            if (toolIterator.Value.ToolReputationTreshold <= Player.Instance.PlayerRep)
            {
                dictOfUnlockedTools.Add(toolIterator.Key, toolIterator.Value);
                Debug.Log($"{this.GameManagerStamp()}+1 tool unlocked", this);
            }
        }
    }
    #endregion
    
    


    #region InfoBox
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
        infoBoxMoneyBaseReward.text = foundContract.MoneyBaseReward.ToString();
        Text infoBoxReputationBaseReward = GameObject.FindGameObjectWithTag("ReputationBaseRewardValue").GetComponent<Text>();
        infoBoxReputationBaseReward.text = foundContract.ReputationBaseReward.ToString();

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
    #endregion

    #region HideoutInfoBoxTools
    //Method to display the tool description on the hideout info box while hovering the tool in the hideout Panel
    public void HideoutInfoBoxDisplay(int linkedToolId, GameObject hideoutToolButton)
    {
        Canvas hideoutCanvas = FindObjectOfType<Canvas>();
        Tools foundTool = dictOfAllTools[linkedToolId];

        hideoutToolButtonPosition = hideoutToolButton.transform.position;
        hideoutInfoBoxOffSet = hideoutToolButtonPosition + new Vector3(0, 200, 0);
        GameObject hideoutInfoBoxPrefabClone = Instantiate(hideoutInfoBoxPrefab, hideoutInfoBoxOffSet, Quaternion.identity, hideoutCanvas.transform);

        Text hideoutInfoBoxReputationTreshold = GameObject.FindGameObjectWithTag("HideoutToolReputationTreshold").GetComponent<Text>();
        hideoutInfoBoxReputationTreshold.text = foundTool.ToolReputationTreshold.ToString();
        //Elements of info box of tools to display
    }


    //Method to destroy the hideout info box clone after hovering
    public void destroyHideoutInfoBox()
    {
        GameObject instantiatedHideoutInfoBox = GameObject.Find("HideoutInfoBoxTools(Clone)");
        if (instantiatedHideoutInfoBox)
        {
            Destroy(instantiatedHideoutInfoBox);
        }
    }
    #endregion

    #region HideoutInfoBoxCrew
    //Method to display the Crew Description on the hideout info box while hovering the crew member in the hideout panel
    public void HideoutCrewInfoBoxDisplay(int linkedCrewId, GameObject hideoutCrewButton)
    {
        Canvas hideoutCanvas = FindObjectOfType<Canvas>();
        GameObject foundCrewMember = dictOfAllCrewMembers[linkedCrewId];

        hideoutCrewButtonPosition = hideoutCrewButton.transform.position;
        hideoutCrewInfoBoxOffSet = hideoutCrewButtonPosition + new Vector3(350, 0, 0);
        GameObject hideoutCrewInfoBoxPrefabClone = Instantiate(hideoutCrewInfoBoxPrefab, hideoutCrewInfoBoxOffSet, Quaternion.identity, hideoutCanvas.transform);

        //Elements of crew info box to display needed to add on 16/03
    }

    //Method to destroy the hideout crew info box clone after hovering
    public void destroyHideoutCrewInfoBox()
    {
        GameObject instantiatedHideoutCrewInfoBox = GameObject.Find("HideoutInfoBoxCrew(Clone)");
        if (instantiatedHideoutCrewInfoBox)
        {
            Destroy(instantiatedHideoutCrewInfoBox);
        }
    }
    #endregion


    #region Contract
    //Method to display all the contract informations on the side and bottom description bar in the selectionContract Panel
    public void ContractAllInfoDisplay(int linkedContractId)
    {
        Contract foundContract = dictOfContracts[linkedContractId];

        Text allInfoDisplayContractName = GameObject.FindGameObjectWithTag("AllDisplay.ContractName").GetComponent<Text>();
        allInfoDisplayContractName.text = foundContract.contractName;
        Text allInfoDisplayContractDescription = GameObject.FindGameObjectWithTag("AllDisplay.ContractDescription").GetComponent<Text>();
        allInfoDisplayContractDescription.text = foundContract.ContractDescription;
        Text allInfoDisplayContractMainObjective = GameObject.FindGameObjectWithTag("AllDisplay.ContractMainObjective").GetComponent<Text>();
        allInfoDisplayContractMainObjective.text = foundContract.ContractMainObjective;
        Text allInfoDisplayContractConstraints = GameObject.FindGameObjectWithTag("AllDisplay.ContractConstraints").GetComponent<Text>();
        allInfoDisplayContractConstraints.text = foundContract.ContractConstraints;
        Text allInfoDisplayContractBonusObjective = GameObject.FindGameObjectWithTag("AllDisplay.ContractBonusObjective").GetComponent<Text>();
        allInfoDisplayContractBonusObjective.text = foundContract.ContractBonusObjective;
        Text allInfoDisplayContractBaseFundReward = GameObject.FindGameObjectWithTag("AllDisplay.ContractBaseFundReward").GetComponent<Text>();
        allInfoDisplayContractBaseFundReward.text = foundContract.MoneyBaseReward.ToString();
        Text allInfoDisplayContractBaseReputationReward = GameObject.FindGameObjectWithTag("AllDisplay.ContractBaseReputationReward").GetComponent<Text>();
        allInfoDisplayContractBaseReputationReward.text = foundContract.ReputationBaseReward.ToString();
        Text allInfoDisplayContractBonusFundReward = GameObject.FindGameObjectWithTag("AllDisplay.ContractBonusFundReward").GetComponent<Text>();
        allInfoDisplayContractBonusFundReward.text = foundContract.MoneyBonusReward.ToString();
        Text allInfoDisplayContractBonusReputationReward = GameObject.FindGameObjectWithTag("AllDisplay.ContractBonusReputationReward").GetComponent<Text>();
        allInfoDisplayContractBonusReputationReward.text = foundContract.ReputationBonusReward.ToString();
        Text allInfoDisplayContractReputationTreshold = GameObject.FindGameObjectWithTag("AllDisplay.ContractReputationTreshold").GetComponent<Text>();
        allInfoDisplayContractReputationTreshold.text = foundContract.ReputationTreshold.ToString();

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
        planningDisplayContractMainObjective.text = selectedContract.ContractMainObjective;
        Text planningDisplayContractConstraints = GameObject.FindGameObjectWithTag("PlanningPhase.contractConstraints").GetComponent<Text>();
        planningDisplayContractConstraints.text = selectedContract.ContractConstraints;
        Text planningDisplayContractBonusObjective = GameObject.FindGameObjectWithTag("PlanningPhase.contractBonusObjective").GetComponent<Text>();
        planningDisplayContractBonusObjective.text = selectedContract.ContractBonusObjective;

    }
    #endregion

    
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Debug.LogWarning($"{this.GameManagerStamp()} instance already created");
            Destroy(this);
            return;
        }

        dictOfContracts = new Dictionary<int, Contract>();
        dictOfAllTools = new Dictionary<int, Tools>();
        dictOfUnlockedTools = new Dictionary<int, Tools>();
        dictOfAllCrewMembers = new Dictionary<int, GameObject>();
        dictOfUnlockedCrewMembers = new Dictionary<int, GameObject>();

        foreach (GameObject currentContractObject in listOfContractsPrefab)
        {
            Contract currentContractComponent = currentContractObject.GetComponent<Contract>();
            dictOfContracts.Add(currentContractComponent.contractId, currentContractComponent); 
            //Debug.Log("+1ContractInDict");
        }

        dictOfAllTools = ToolsDictionary.InitializeDict(listOfToolsPrefab);
        dictOfAllCrewMembers = CrewDict.InitializeDict(listOfCrewMemberPrefab);

        /*foreach (Crew availableCrewMember in listOfCrewMemberPrefab)
        {
            dictOfAllCrewMembers.Add(c.CrewId, c);
            Debug.Log("+1 Crew Member");
        }*/

        //Add save informations before RaisePlayerRep and ResolveUnlockedTools
        SaveManager.LoadSave();
    }

    private void Start()
    {
        ResolveUnlockedTools();
        DisplayUnlockedTools();
        ResolveUnlockedCrewMembers();
        DisplayUnlockedCrewMembers();
    }
}

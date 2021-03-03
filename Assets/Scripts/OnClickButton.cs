using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickButton : MonoBehaviour
{
    public Image buttonImage;
    public Tools selectedTool;
    public Crew selectedCrew;


    //Method to display the clicked tool in the selected tool slot
    public void DisplaySelectedTool()
    {
        Image selectedToolImage = GameObject.FindGameObjectWithTag("SelectedToolDisplayer").GetComponent<Image>();
        selectedToolImage.sprite = buttonImage.sprite;
        selectedToolImage.color = buttonImage.color;
        //Debug.Log("test");
        GameObject.FindGameObjectWithTag("TempGameManager").GetComponent<TempGameManager>().currentSelectedTool = selectedTool;
        //Debug.Log("test1");
        
        PlayerPrefs.SetInt("toolId", selectedTool.ToolsId);
        PlayerPrefs.Save();
        
        Debug.Log( $"{this.GetStamp("ffffff")} Tools id : " + PlayerPrefs.GetInt("toolId"), this);
    }

    //Method to display the clicked crew in the selected crew slot
    public void DisplaySelectedCrew()
    {
        Image selectedCrewMemberImage = GameObject.FindGameObjectWithTag("SelectedCrewMemberDisplayer").GetComponent<Image>();
        selectedCrewMemberImage.sprite = buttonImage.sprite;
        selectedCrewMemberImage.color = buttonImage.color;
        GameObject.FindGameObjectWithTag("TempGameManager").GetComponent<TempGameManager>().currentSelectedCrew = selectedCrew;
        PlayerPrefs.SetInt("crewID", selectedCrew.crewId);
        PlayerPrefs.Save();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

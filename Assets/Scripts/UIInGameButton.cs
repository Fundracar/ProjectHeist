using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInGameButton : MonoBehaviour
{
    

    public void OnReplayBtn()
    {
        SceneManager.LoadScene(GameManager.Instance.Contract.SceneName);
    }

    public void OnMenuBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

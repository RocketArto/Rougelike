using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject choosePlayerPanel;
    public GameObject settingsPanel;

    public GameObject continueButton;

    private void Start()
    {
        int level = PlayerPrefs.GetInt("CurrentLevel");
        if (level == 0)
        {
            continueButton.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
        //SceneManager.LoadScene(1);
    }

    public void NewGame() {
        PlayerPrefs.DeleteAll();
        OpenPlayerPanel();
    }

    public void OpenPlayerPanel()
    {
        choosePlayerPanel.SetActive(true);
    }

    public void ClosePlayerPanel()
    {
        //choosePlayerPanel.SetActive(false);
        SceneManager.LoadSceneAsync(1);
        //CameraFollowPlayer.Instance.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

}

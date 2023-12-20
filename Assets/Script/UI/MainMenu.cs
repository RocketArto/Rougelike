using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        CameraFollowPlayer.Instance.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void ExitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

}

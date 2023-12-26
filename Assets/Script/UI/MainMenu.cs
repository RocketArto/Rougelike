using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject choosePlayerPanel;

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
        //SceneManager.LoadScene(1);
    }

    public void OpenPlayerPanel()
    {
        choosePlayerPanel.SetActive(true);
    }

    public void ClosePlayerPanel()
    {
        choosePlayerPanel.SetActive(false);
        CameraFollowPlayer.Instance.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void ExitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

}

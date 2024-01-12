//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class IngameUI : MonoBehaviour
{
    public static IngameUI Instance;

    public GameObject loadingPanel;
    public GameObject pausePanel;
    public GameObject floorClearedPanel;
    public GameObject floorFailedPanel;
    public TextMeshProUGUI livesText;
    [SerializeField] Image[] avatarList;

    void Start()
    {
        Instance = this;
        livesText.text = "Lives: " + PlayerStats.Instance.lives;
    }

    public void SetAvatar()
    {
        foreach (Image avatar in avatarList)
        {
            avatar.gameObject.SetActive(false);
        }
        avatarList[PlayerStats.Instance.currentPlayer].gameObject.SetActive(true);
    }
    

    //PauseGamePanel
    public void PauseGame() {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ClosePausePanel()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnToMainhome()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }

    //FloorClearedPanel
    public void FloorCleared()
    {
        Time.timeScale = 0;
        loadingPanel.SetActive(true);
        //floorClearedPanel.SetActive(true);
        ReturnWin();
    }

    public void ReturnWin()
    {
        //loadingPanel.SetActive(true);
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }

    //FloorFailedPanel
    public void FloorFailed()
    {
        Time.timeScale = 0;
        floorFailedPanel.SetActive(true);
    }

    public void ReturnLose()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
    }
}

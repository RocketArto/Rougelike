using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class IngameUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    [SerializeField] Image[] avatarList;

    void Start()
    {
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
    public void ReturnToMainhome()
    {
        SceneManager.LoadScene(0);
    }


}

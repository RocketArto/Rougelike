using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGamePortal : MonoBehaviour
{
    int currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Invoke("EndGame", 0.1f);
        }
            
    }

    private void EndGame()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel") + 1;
        PlayerPrefs.SetInt("lives", PlayerStats.Instance.lives);
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("CurrentPlayer", PlayerStats.Instance.currentPlayer);
        PlayerPrefs.SetInt("GoldValue", PlayerStats.Instance.coin);
        PlayerPrefs.SetInt("GemValue", PlayerStats.Instance.gem);
        IngameUI.Instance.FloorCleared();
    }
}

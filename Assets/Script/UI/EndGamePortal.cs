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
        EndGame();
    }

    private void EndGame()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel") + 1;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        SceneManager.LoadScene(0);
    }
}

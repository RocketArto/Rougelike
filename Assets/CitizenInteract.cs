using Edgar.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenInteract : MonoBehaviour
{
    public bool interactable = false;

    private void Update()
    {
        TakeInput();
    }

    public void TakeInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactable == true)
            {
                print("interact");
                //code for interaction here
                //PauseGame();
                PlayerStats.Instance.lives += 1;
                PlayerStats.Instance.livesText.text = "Lives: " + PlayerStats.Instance.lives;
                PlayerStats.Instance.p1.transform.position = gameObject.transform.position;
                PlayerStats.Instance.ChangePlayer();
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == false)
        {
            interactable = false;
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogNewPlayer : MonoBehaviour
{
    public int setPlayer;
    public GameObject player;
    public GameObject spawnPoint;
    public void ChangeCurrentPlayer()
    {
        PlayerPrefs.SetInt("CurrentPlayer", setPlayer);
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        spawnPoint.SetActive(false);
        spawnPoint.SetActive(true);

        //CameraFollowPlayer.Instance.player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}

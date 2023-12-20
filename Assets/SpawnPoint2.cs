using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint2 : MonoBehaviour
{
    public int playerIndex;
    [SerializeField] GameObject[] playerList;
    public GameObject sEffect;

    void Awake()
    {
    }

    void OnEnable()
    {

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        playerIndex = PlayerPrefs.GetInt("CurrentPlayer");
        GameObject spawnPlayer = Instantiate(playerList[playerIndex], transform.position, Quaternion.identity);
        CameraFollowPlayer.Instance.player = spawnPlayer.transform;
        PlayerInitialized();
        GameObject spawnEffect = Instantiate(sEffect, gameObject.transform.position, Quaternion.identity);
        CameraFollowPlayer.Instance.player = spawnPlayer.transform;
    }

    public void PlayerInitialized()
    {
        PlayerStats.Instance.health = PlayerStats.Instance.maxHealth;
        PlayerStats.Instance.mana = PlayerStats.Instance.maxMana;
        //KnockEnemies(p1.transform.position);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int playerIndex;
    public GameObject player;
    public GameObject sEffect;

    void Awake()
    {
    }

    void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        GameObject spawnPlayer = Instantiate(player, transform.position, Quaternion.identity);
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

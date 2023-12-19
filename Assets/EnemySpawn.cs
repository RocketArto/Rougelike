using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public int enemyIndex;
    public GameObject enemy;
    public GameObject sEffect;

    void Awake()
    {
    }

    void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        GameObject spawnEnemy = Instantiate(enemy);
        spawnEnemy.transform.position = transform.position;
        //CameraFollowPlayer.Instance.player = spawnPlayer.transform;
        //PlayerInitialized();
        GameObject spawnEffect = Instantiate(sEffect, gameObject.transform.position, Quaternion.identity);
        //CameraFollowPlayer.Instance.player = spawnPlayer.transform;
    }

    public void PlayerInitialized()
    {
        PlayerStats.Instance.health = PlayerStats.Instance.maxHealth;
        PlayerStats.Instance.mana = PlayerStats.Instance.maxMana;
        //KnockEnemies(p1.transform.position);
    }
}

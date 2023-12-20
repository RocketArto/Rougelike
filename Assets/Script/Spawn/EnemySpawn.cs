using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public int enemyIndex;
    public GameObject enemy;
    public GameObject sEffect;
    public SpawnManagerScriptableObject spawnList;
    public int spawnLevel;

    void Awake()
    {
    }

    void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        spawnLevel = PlayerStats.Instance.level;
        if (spawnLevel + 1 > spawnList.spawnList.Length) {
            spawnLevel = spawnList.spawnList.Length - 1;
        }
        GameObject spawnEnemy = Instantiate(spawnList.spawnList[Random.Range(0, spawnLevel+1)], transform.position, Quaternion.identity);
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

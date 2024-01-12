using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint2 : MonoBehaviour
{
    public int playerIndex;
    public SpawnManagerScriptableObject spawnList;
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
        GameObject spawnPlayer = Instantiate(spawnList.spawnList[playerIndex], transform.position, Quaternion.identity);
        GameObject spawnEffect = Instantiate(sEffect, gameObject.transform.position, Quaternion.identity);
        //CameraFollowPlayer.Instance.player = spawnPlayer.transform;
    }


}

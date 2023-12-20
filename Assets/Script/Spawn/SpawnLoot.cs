using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLoot : MonoBehaviour
{
    [SerializeField] GameObject[] lootList;
    [SerializeField] int maxLootQuantity;
    //SpawnManagerScriptableObject lootList2;

    void Start()
    {
        int count = Random.Range(1, maxLootQuantity);
        for (int i = 0; i < count; i++)
        {
            print(i);
            spawnLoot(transform.position);
        }
        //gameObject.SetActive(false);
    }

    private void spawnLoot(Vector2 position)
    {
        print("Hello");
        float splashSpeed = 1f;
        GameObject spawnedObject = Instantiate(lootList[Random.Range(0,lootList.Length)], position, Quaternion.identity);
        float axisX = Random.Range(-5f, 5f);
        float axisY = Random.Range(-5f, 5f);
        Vector2 direction = new Vector2(axisX,axisY);
        Vector2 tempVec = spawnedObject.transform.position;
        spawnedObject.GetComponent<Rigidbody2D>().velocity = direction / (direction.magnitude) * splashSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangThrow : MonoBehaviour
{
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    public float manaCost;

    public int checkPlayerDir = 4;
    public Vector2 boomerangDir = Vector2.zero;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 direction = transform.position;
        checkPlayerDir = gameObject.GetComponent<PlayerMovement>().face;
        if (checkPlayerDir == 1)
        {
            direction = Vector2.up;
        } else if (checkPlayerDir == 2)
        {
            direction = Vector2.left;
        }
        else if (checkPlayerDir == 3) { 
            direction = Vector2.down;
        }
        else if (checkPlayerDir == 4)
        {
            direction = Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerStats.Instance.checkMana(manaCost) == true)
            {
                GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
                spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
                spell.GetComponent<BoomerangBehave>().damage = Random.Range(minDamage, maxDamage);
                PlayerStats.Instance.loseMana(manaCost);
            }
        }
    }

    private GameObject FindClosestEnemyWithinRange(float range)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance <= range && curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }
}

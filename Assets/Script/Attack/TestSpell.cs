using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : MonoBehaviour
{

    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    public float manaCost;
    public float curMana;
    public float detectRange;


    void Update(){

        Vector2 direction = transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = transform.position;
            GameObject targeted = FindClosestEnemyWithinRange(detectRange);
            Vector2 newTarLocation = mousePos;
            if (targeted != null) {
                newTarLocation = targeted.transform.position;
                direction = (newTarLocation - myPos).normalized;
                if (PlayerStats.Instance.checkMana(manaCost) == true)
                {
                    GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
                    spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
                    spell.GetComponent<TestProjectile>().damage = Random.Range(minDamage, maxDamage);
                    PlayerStats.Instance.loseMana(manaCost);
                }
            }
            else
            {
                if (gameObject.GetComponent<PlayerMovement>().face == 1) { }


                direction = transform.position;
                if (PlayerStats.Instance.checkMana(manaCost) == true)
                {
                    float axisX = Random.Range(-10f, 10f);
                    float axisY = Random.Range(-10f, 10f);
                    Vector2 randomVector = new Vector2(axisX, axisY);
                    GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
                    spell.GetComponent<Rigidbody2D>().velocity = randomVector/ (randomVector.magnitude) * projectileForce;
                    spell.GetComponent<TestProjectile>().damage = Random.Range(minDamage, maxDamage);
                    PlayerStats.Instance.loseMana(manaCost);
                }
            }
            
            //Vector2 direction = (mousePos - myPos).normalized;
            
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

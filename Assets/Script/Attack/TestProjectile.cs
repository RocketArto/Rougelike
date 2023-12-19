using Edgar.Benchmarks.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") {
            if (collision.tag == "Enemy")
            {
                if (collision.GetComponent<EnemyRecieveDamage>() != null)
                {
                    collision.GetComponent<EnemyRecieveDamage>().DealDamage(damage);
                }
                //gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }

}

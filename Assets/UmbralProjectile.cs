using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbralProjectile : MonoBehaviour
{
    public float maxDamage;
    public float minDamage;

    void Start() {
        Invoke("Deactive", 0.8f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.GetComponent<EnemyRecieveDamage>() != null)
            {
                collision.GetComponent<EnemyRecieveDamage>().DealDamage(Random.Range(maxDamage,minDamage));
            }
        }
    }

    public void Deactive() { 
        gameObject.SetActive(false);
    }
}

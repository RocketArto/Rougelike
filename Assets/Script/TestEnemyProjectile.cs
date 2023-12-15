using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyProjectile : MonoBehaviour
{
    public float damage;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.tag);
        if (collision.tag != "Enemy"){
            if (collision.tag == "Player"){
                PlayerStats.Instance.DealDamage(damage);
            }
            gameObject.SetActive(false);
        }
    }
}

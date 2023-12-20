using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    public float minDamage;
    public float maxDamage;





    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && (PlayerStats.Instance.iframe != true))
        {
            print("Hello");
            PlayerStats.Instance.DealDamage(Random.Range(minDamage,maxDamage));
        }
    }
}

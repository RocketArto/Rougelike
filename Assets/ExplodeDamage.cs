using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeDamage : MonoBehaviour
{
    public float minDamage;
    public float maxDamage;
    public float explodeRange;

    void Start()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, explodeRange);
        foreach (Collider2D player in hitPlayer)
        {
            if (player.CompareTag("Player"))
            {
                PlayerStats.Instance.DealDamage(Random.Range(minDamage, maxDamage));
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BatEnemy : MonoBehaviour
{
    public float minDamage;
    public float maxDamage;
    public float attackRange;
    public LayerMask Player;
    private bool isAttack = true;

    
    private void Update()
    {
        if (isAttack == true && (Vector2.Distance(transform.position,PlayerStats.Instance.p1.transform.position) <= attackRange)) 
            Attack();
    }

    public void Attack()
    {
        print("Attack");
        PlayerStats.Instance.DealDamage(Random.Range(minDamage, maxDamage));
        isAttack = false;
        StartCoroutine(WaitToAttack());
    }

    IEnumerator WaitToAttack() { 
        yield return new WaitForSeconds(1.2f);
        isAttack = true;
    }
}

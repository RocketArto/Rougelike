using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 1f;
    public float cooldownTime = 0.7f;
    public string enemyTag = "Enemy"; // using enemy tag
    private bool isOnCooldown = false;
    public Animator animator;
    public GameObject effect;


    void Update()
    {
        if (!isOnCooldown && Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }


    void Attack()
    {
        StartCooldown();
        effect.SetActive(true);
        Invoke(nameof(DeactivateObject), 0.5f);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                enemy.GetComponent<EnemyRecieveDamage>().DealDamage(damage);
            }
        }
        
    }

    void StartCooldown()
    {
        isOnCooldown = true;
        Invoke("ResetCooldown", cooldownTime);
    }

    void ResetCooldown()
    {
        isOnCooldown = false;
    }

    void DeactivateObject()
    {
        effect.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damage;
    public float attackRange;
    public float cooldownTime;
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
        Invoke(nameof(DeactivateEffect), 0.5f);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange);
        Vector2 currPos = gameObject.transform.position;
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                Vector2 temp = enemy.transform.position;
                Vector2 knockDirection = temp - currPos;
                if (enemy.TryGetComponent(out KnockBack knock))
                {
                    knock.Knock(knockDirection);
                }
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

    void DeactivateEffect()
    {
        effect.SetActive(false);
    }

}

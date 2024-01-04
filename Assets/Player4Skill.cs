using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player4Skill : MonoBehaviour
{
    public string enemyTag = "Enemy"; // using enemy tag
    public float damage;
    public float attackRange;
    private void Start()
    {
        Invoke("ClearField", 0.2f);
    }

    private void ClearField()
    {
        CameraShake.Instance.Shake(0.1f, 0.2f);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                for(int i = 0; i < hitEnemies.Length; i++)
                {
                    enemy.GetComponent<EnemyRecieveDamage>().DealDamage(damage);
                }
            }
        }
        GameObject.Destroy(gameObject, 1f);
    }
}

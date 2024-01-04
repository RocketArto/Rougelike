using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Skill : MonoBehaviour
{
    public string enemyTag = "Enemy"; // using enemy tag
    public float attackRange = 3f;
    public int damage;
    public GameObject slamEffect;

    void Start()
    {
        Invoke("KnockInCircle", 0.2f);
    }

    public void KnockInCircle()
    {
        CameraShake.Instance.Shake(0.1f,0.2f);
        GameObject slam = Instantiate(slamEffect,gameObject.transform.position,Quaternion.identity);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                enemy.GetComponent<EnemyRecieveDamage>().DealDamage(damage);
            }
        }
        PlayerStats.Instance.rechargeSkill(gameObject.GetComponent<SkillStats>().skillIndex, gameObject.GetComponent<SkillStats>().rechargeTime);
        GameObject.Destroy(gameObject, 1.5f);
        GameObject.Destroy(slam, 1f);
    }
}

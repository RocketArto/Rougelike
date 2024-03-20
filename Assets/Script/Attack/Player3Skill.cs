using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3Skill : MonoBehaviour
{
    public GameObject projectile;
    public float projectileForce;
    public float minDamage;
    public float maxDamage;

    void Start()
    {
        GameObject spell1 = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector2 direction1 = Vector2.up;
        spell1.GetComponent<Rigidbody2D>().velocity = direction1 * projectileForce;
        spell1.GetComponent<BoomerangBehave>().damage = Random.Range(minDamage, maxDamage);

        GameObject spell2 = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector2 direction2 = Vector2.right;
        spell2.GetComponent<Rigidbody2D>().velocity = direction2 * projectileForce;
        spell2.GetComponent<BoomerangBehave>().damage = Random.Range(minDamage, maxDamage);

        GameObject spell3 = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector2 direction3 = Vector2.left;
        spell3.GetComponent<Rigidbody2D>().velocity = direction3 * projectileForce;
        spell3.GetComponent<BoomerangBehave>().damage = Random.Range(minDamage, maxDamage);

        GameObject spell4 = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector2 direction4 = Vector2.down;
        spell4.GetComponent<Rigidbody2D>().velocity = direction4 * projectileForce;
        spell4.GetComponent<BoomerangBehave>().damage = Random.Range(minDamage, maxDamage);
        GameObject.Destroy(gameObject, 3f);

        PlayerStats.Instance.rechargeSkill(gameObject.GetComponent<SkillStats>().skillIndex, gameObject.GetComponent<SkillStats>().rechargeTime);
    }
}

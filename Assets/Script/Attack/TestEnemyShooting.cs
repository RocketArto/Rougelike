using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyShooting : MonoBehaviour
{
    public GameObject projectile;
    public Transform player;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    public float cooldown;
    public float detectRange;
    private Animator animator;

    void Start(){
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ShootPlayer());
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    IEnumerator ShootPlayer(){
        yield return new WaitForSeconds(cooldown);
        Vector2 myPos = transform.position;
        Vector2 targetPos = player.position;
        if (player != null && Vector2.Distance(myPos, targetPos) <= detectRange)
        {
            animator.SetBool("Awaken", true);
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 direction = (targetPos - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<TestEnemyProjectile>().damage = Random.Range(minDamage,maxDamage);
        }
        StartCoroutine(ShootPlayer());
    }
}

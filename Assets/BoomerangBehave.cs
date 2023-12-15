using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBehave : MonoBehaviour
{
    public bool onReturn = false;
    public float damage;
    public Vector2 direction;
    public Transform player;
    public float speed;

    void Start()
    {
        Invoke("WaitAndReturn", 3f);
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (onReturn == true) Move();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.GetComponent<EnemyRecieveDamage>() != null)
            {
                collision.GetComponent<EnemyRecieveDamage>().DealDamage(damage);
            }
        }

        if (collision.tag == "Player" && onReturn == true)
        {
            gameObject.SetActive(false);
        }
    }

    public void WaitAndReturn()
    {
        onReturn = true;

    }

    public void Move()
    {
        direction = player.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * speed * Time.deltaTime);
    }
}

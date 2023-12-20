using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class BatMovement : MonoBehaviour
{
    public Animator animator;
    public Vector2 direction = Vector2.zero;
    public Vector2 chaseDirection = Vector2.zero;
    public float speed = 0.5f;
    public float runtime = 0.7f;

    public bool isChasing = false;
    public float detectRange = 1f;
    public float chaseRange = 2f;
    public Transform player;


    void Start()
    {
        StartCoroutine(GetDirection());
    }

    

    void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (Vector2.Distance(transform.position,player.position) <= detectRange)
        {
            isChasing = true;
        } else if (Vector2.Distance(transform.position, player.position) > chaseRange)
        {
            isChasing = false;
        }
        Move();
    }

    public void ChooseDirection()
    {
        float axisX = Random.Range(-10f, 10f);
        float axisY = Random.Range(-10f, 10f);
        direction = new Vector2(axisX, axisY);
        direction.Normalize();
    }

    public void Move()
    {
        if (isChasing == false) {
            transform.Translate(direction * speed * Time.deltaTime);
            SetAnimatorMovement(direction);
        }
        else
        {
            chaseDirection = player.position - transform.position;
            chaseDirection.Normalize();
            transform.Translate(chaseDirection * speed * Time.deltaTime);
            SetAnimatorMovement(chaseDirection);
        }
    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetFloat("yDir", direction.y);
        if (direction.x <= 0.1f && direction.x > -0.1f) return;
        animator.SetFloat("xDir", direction.x);
    }

    IEnumerator GetDirection()
    {
        yield return new WaitForSeconds(runtime);
        ChooseDirection();
        StartCoroutine(GetDirection());
    }
}

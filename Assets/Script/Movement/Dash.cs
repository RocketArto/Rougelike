using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dashSpeed;
    public float dashTime;
    public float startDashTime;
    public Vector2 direction;
    public float axisX;
    public float axisY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            axisY = -1;
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            axisY = 1;
        } else
        {
            axisY = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            axisX = -1;
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            axisX = 1;
        }
        else
        {
            axisX = 0;
        }
        direction.x = axisX;
        direction.y = axisY;

        if (Input.GetKeyDown(KeyCode.D))
        {
            startDash();
        }
    }

    public void startDash()
    {
        rb.velocity = direction;
    }
}

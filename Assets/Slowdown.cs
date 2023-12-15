using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowdown : MonoBehaviour
{
    public bool pickable = false;
    public float slowDownTime;

    private void Start()
    {
        StartCoroutine(SlowDown());
    }

    IEnumerator SlowDown()
    {
        float elapsedTime = 0f;
        Vector2 initialVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;

        var rb = gameObject.GetComponent<Rigidbody2D>();

        while (elapsedTime < slowDownTime)
        {
            rb.velocity = initialVelocity * (1 - (elapsedTime / slowDownTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        pickable = true;
    }
}

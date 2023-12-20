using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool onKnock = false;
    public float slowDownTime = 2f;


    public void Knock(Vector2 knockDirection)
    {
        //get some velocity
        knockDirection.Normalize();
        gameObject.GetComponent<Rigidbody2D>().velocity = knockDirection*10f;
        onKnock = true;

        //apply damping
        StartCoroutine(SlowDown());
    }

    IEnumerator SlowDown()
    {
        float elapsedTime = 0f;

        Vector2 initialVelocity = Vector2.zero;
        if (gameObject.TryGetComponent(out Rigidbody2D rigid))
        {
            initialVelocity = rigid.velocity;
        }
        //Vector2 initialVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;

        var rb = gameObject.GetComponent<Rigidbody2D>();

        while (elapsedTime < slowDownTime)
        {
            rb.velocity = initialVelocity * (1 - (elapsedTime / slowDownTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        onKnock = false;
    }
}

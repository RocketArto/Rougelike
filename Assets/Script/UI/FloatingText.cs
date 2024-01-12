using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private Rigidbody2D rb;
    private TMP_Text damageValue;

    public float initXvelo = 1.5f;
    public float initYvelo = 3.5f;
    public float lifeTime = 0.4f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        damageValue = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(Random.Range(-initXvelo, initXvelo), initYvelo);
        Destroy(gameObject, lifeTime);
    }

    private void SetMessage(float msg)
    {
        int message = Mathf.RoundToInt(msg);
        damageValue.text = ""+message;
    }
}

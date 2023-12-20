using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyRecieveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject healthBar;
    public UnityEngine.UI.Slider healthBarSlider;

    public GameObject lootDrop;

    //public float throwForce = 1f;
    //public float dampingFactor = 0.97f;
    public float lifetime = 0.2f;

    public float slowDownTime = 1f;

    void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(float damage)
    {
        healthBar.SetActive(true);
        health -= damage;
        CheckDeath();
        gameObject.GetComponent<FlashEffect>().Flash();
        healthBarSlider.value = CaculateHealthPercentage();
    }

    private void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {   
            //spawnLoot(transform.position);
            GameObject spawnedLoot = Instantiate(lootDrop, transform.position, Quaternion.identity);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    private float CaculateHealthPercentage() {
        return (health / maxHealth);
    }

}
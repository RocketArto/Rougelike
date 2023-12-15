using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
//singleton
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public float health;
    public float maxHealth;
    public float mana;
    public float maxMana;
    public float hpHealSpeed;
    public float manaHealSpeed;
    public float cooldown;

    public PlayerMovement player;
    public GameObject p1;

    public Image healthBarSlider;
    public Image manaBarSlider;

    public int coin;
    public int gem;
    public bool iframe = false;

    [SerializeField] GameObject[] playerList;
    public GameObject respawn;
    public int currentPlayer = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FindPlayer(TestEnemyShooting enemy)
    {
        enemy.player = this.player.transform;
    }

    void Start()
    {
        health = maxHealth;
        StartCoroutine(RecoverMana());
        p1 = GameObject.FindGameObjectWithTag("Player");
    }


    //quan ly HP
    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        healthBarSlider.fillAmount = CaculateHealthPercentage();
        StartCoroutine(IsIframe(0.3f));
        p1.GetComponent<FlashEffect>().Flash();
    }

    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        healthBarSlider.fillAmount = CaculateHealthPercentage();
    }


    public void CheckOverheal()
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
            //player.gameObject.SetActive(false);
            ChangePlayer();
        }
    }

    private float CaculateHealthPercentage()
    {
        return (health / maxHealth);
    }


    //Quan ly Mana

    public bool checkMana(float manaCost)
    {
        if (manaCost >= mana) return false;
        else return true;
    }

    public void loseMana(float manaLost)
    {
        mana -= manaLost;
        manaBarSlider.fillAmount = CaculateManaPercentage();
    }



    public void HealCharacterMana(float rev)
    {
        mana += rev;
        CheckOverhealMana();
        manaBarSlider.fillAmount = CaculateManaPercentage();
    }



    public void CheckOverhealMana()
    {
        if (mana > maxMana)
        {
            mana = maxMana;
        }
    }


    private float CaculateManaPercentage()
    {
        return (mana / maxMana);
    }


    //Hoi HP&Mana

    IEnumerator RecoverMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            HealCharacter(hpHealSpeed);
            HealCharacterMana(manaHealSpeed);
        }
    }

    //Iframe management
    IEnumerator IsIframe(float iframeTime)
    {
        if (iframe == false)
        {
            iframe = true;
            p1.GetComponent<FlashEffect>().Flash();
            yield return new WaitForSeconds(iframeTime);
            iframe = false;
        }
        yield return new WaitForSeconds(iframeTime);
    }

    IEnumerator Flashing()
    {
        while (true)
        {

            if (iframe == true)
            {
                yield return new WaitForSeconds(player.GetComponent<FlashEffect>().duration);
            }
        }
    }

    //change player
    public void ChangePlayer()
    {
        currentPlayer = (currentPlayer + 1) % (playerList.Length);
        Vector2 currLocation = p1.transform.position;
        Destroy(p1);
        GameObject spawnEffect = Instantiate(respawn, currLocation, Quaternion.identity);
        GameObject spawnPlayer = Instantiate(playerList[currentPlayer], currLocation, Quaternion.identity);
        p1 = spawnPlayer;
        CameraFollowPlayer.Instance.player = p1.transform;
        PlayerInitialized();
    }

    //quan ly player
    public void PlayerInitialized()
    {
        health = maxHealth;
        mana = maxMana;
    }

}

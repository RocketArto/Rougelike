using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int level;
    public int lives;
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
    public GameObject spawnPoint;
    public int currentPlayer;

    public string enemyTag = "Enemy";
    public float knockDamage = 100f;

    public TextMeshProUGUI livesText;

    [SerializeField] Image[] avatarList;

    private void Awake()
    {
        Instance = this;
        level = PlayerPrefs.GetInt("CurrentLevel");
        lives = 1;
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
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        currentPlayer = spawnPoint.GetComponent<SpawnPoint>().playerIndex;
        SetAvatar();
    }

    private void Update()
    {
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
            lives -= 1;
            if (lives <= 0)
            {
                SceneManager.LoadSceneAsync(0);
            }
            livesText.text = "Lives: " + PlayerStats.Instance.lives;
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


    //Recover Mana

    IEnumerator RecoverMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            //HealCharacter(hpHealSpeed);
            HealCharacterMana(manaHealSpeed);
        }
    }

    

    //change player
    public void ChangePlayer()
    {
        currentPlayer = (currentPlayer + 1) % (playerList.Length);
        Debug.Log(currentPlayer);
        Vector2 currLocation = p1.transform.position;
        Destroy(p1);
        GameObject spawnEffect = Instantiate(respawn, currLocation, Quaternion.identity);
        GameObject spawnPlayer = Instantiate(playerList[currentPlayer], currLocation, Quaternion.identity);
        p1 = spawnPlayer;
        CameraFollowPlayer.Instance.player = p1.transform;
        PlayerInitialized();
    }

    //manage player
    public void PlayerInitialized()
    {
        health = maxHealth;
        mana = maxMana;
        KnockEnemies(p1.transform.position);
        SetAvatar();
        StartCoroutine(IsIframe(0.3f));
        p1.GetComponent<FlashEffect>().Flash();
    }

    public void KnockEnemies(Vector2 center) {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(center, 0.5f);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                Vector2 temp = enemy.transform.position;
                Vector2 knockDirection = temp - center;
                //using TryGetComponent is more optimal
                if (enemy.TryGetComponent(out KnockBack knock)) {
                    knock.Knock(knockDirection);
                }
                //enemy.GetComponent<KnockBack>().Knock(knockDirection);
                enemy.GetComponent<EnemyRecieveDamage>().DealDamage(knockDamage);
            }
        }
    }

    public void SetAvatar()
    {
        foreach (Image avatar in avatarList)
        {
            avatar.gameObject.SetActive(false);
        }
        avatarList[currentPlayer].gameObject.SetActive(true);
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

}

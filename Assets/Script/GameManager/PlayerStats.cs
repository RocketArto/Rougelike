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
using DamageNumbersPro;
using System.Timers;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public DamageNumber healNumberPrefab;
    public DamageNumber numberPrefab;
    
    public int level;
    public int lives;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI gemText;

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

    public Image[] skillSlider;
    public bool[] isRecharge;
    public GameObject[] skills;
    Coroutine[] coroutines = new Coroutine[] {null,null,null};

    public int coin;
    public int gem;
    public bool iframe = false;

    //PlayerList
    public SpawnManagerScriptableObject spawnList;
    //SkillList
    public SpawnManagerScriptableObject skillList;
    //AvatarList
    public ImageSourceListScriptableObject imageSource;

    public GameObject respawn;
    public GameObject spawnPoint;
    public GameObject tomb;
    public int currentPlayer;

    public string enemyTag = "Enemy";
    public float knockDamage = 100f;
    public bool isIngame;

    

    [SerializeField] Image[] avatarList;

    private void Awake()
    {
        Instance = this;
        level = PlayerPrefs.GetInt("CurrentLevel");
        lives = PlayerPrefs.GetInt("lives");
        coin = PlayerPrefs.GetInt("GoldValue");
        gem = PlayerPrefs.GetInt("GemValue");
        if (lives == 0)
        {
            lives = 1;
        }
    }

    public void FindPlayer(TestEnemyShooting enemy)
    {
        enemy.player = this.player.transform;
    }

    void Start()
    {
        isRecharge = new bool[] { false, false, false };
        levelText.text = "Floor: " + (level + 1);
        //goldText.text = "" + PlayerPrefs.GetInt("goldValue");
        //gemText.text = "" + PlayerPrefs.GetInt("gemValue");
        skillSlider[0].fillAmount = 0;
        skillSlider[1].fillAmount = 0;
        skillSlider[2].fillAmount = 0;
        health = maxHealth;
        StartCoroutine(RecoverMana());
        
        if (GameObject.FindGameObjectWithTag("Player") != null) {
            p1 = GameObject.FindGameObjectWithTag("Player");
        }
        //p1 = GameObject.FindGameObjectWithTag("Player");
        if (GameObject.FindGameObjectWithTag("SpawnPoint") != null) {
            spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        }
        //spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        
        //TryGet is more optimal
        if (spawnPoint.TryGetComponent(out SpawnPoint sPoint))
        {
            currentPlayer = spawnPoint.GetComponent<SpawnPoint>().playerIndex;
        }
        //currentPlayer = spawnPoint.GetComponent<SpawnPoint>().playerIndex;
        SetAvatar();

    }

    private void Update()
    {
        goldText.text = "" + coin;
        gemText.text = "" + gem;
        p1 = GameObject.FindGameObjectWithTag("Player");
        TakeInput();
    }

    public void TakeInput()
    {
        if ( Input.GetKeyDown("1") && isRecharge[0] == false)
        {
            if ((skills[0] != null)&&(isIngame==true))
            {
                Vector2 offset = new Vector2(Random.Range(-10,10), Random.Range(-10, 10));
                offset = offset.normalized * 0.7f;
                Vector2 playerPos = p1.transform.position;
                GameObject skill_1 = Instantiate(skills[0],playerPos+offset,Quaternion.identity);
                skill_1.GetComponent<SkillStats>().skillIndex = 0;
                skillSlider[0].fillAmount = 1;
                isRecharge[0] = true;
            }
        }
        else if (Input.GetKeyDown("2") && isRecharge[1] == false)
        {
            if ((skills[1] != null) && (isIngame == true))
            {
                Vector2 offset = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
                offset = offset.normalized * 0.7f;
                Vector2 playerPos = p1.transform.position;
                GameObject skill_2 = Instantiate(skills[1], playerPos + offset, Quaternion.identity);
                skill_2.GetComponent<SkillStats>().skillIndex = 1;
                skillSlider[1].fillAmount = 1;
                isRecharge[1] = true;
            }
        }
        else if (Input.GetKeyDown("3") && isRecharge[2] == false)
        {
            if ((skills[2] != null) && (isIngame == true))
                {
                Vector2 offset = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
                offset = offset.normalized * 0.7f;
                Vector2 playerPos = p1.transform.position;
                GameObject skill_3 = Instantiate(skills[2], playerPos + offset, Quaternion.identity);
                skill_3.GetComponent<SkillStats>().skillIndex = 2;
                skillSlider[2].fillAmount = 1;
                isRecharge[2] = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)&&isIngame)
        {
            IngameUI.Instance.PauseGame();
        }
    }

    //quan ly HP
    public void DealDamage(float damage)
    {
        health -= damage;
        Vector2 offsetDamPos = new Vector2(0,0.3f); 
        Vector2 currentPos = p1.transform.position;
        DamageNumber damageNumber = numberPrefab.Spawn(currentPos+offsetDamPos, damage);
        CheckDeath();
        healthBarSlider.fillAmount = CaculateHealthPercentage();
        StartCoroutine(IsIframe(0.3f));
        p1.GetComponent<FlashEffect>().Flash();
    }

    public void HealCharacter(float heal)
    {
        health += heal;
        Vector2 offsetDamPos = new Vector2(0, 0.3f);
        Vector2 currentPos = p1.transform.position;
        DamageNumber healNumber = healNumberPrefab.Spawn(currentPos + offsetDamPos, heal);
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
            livesText.text = "Lives: " + PlayerStats.Instance.lives;
            if (lives <= 0)
            {
                GameObject spawnTomb = Instantiate(tomb,p1.transform.position,Quaternion.identity);
                p1.SetActive(false);
                Invoke("EndGamePanelOpen", 3f);
                //SceneManager.LoadSceneAsync(0);
            }
            else
            {
                ChangeBackPlayer();
            }
        }
    }

    private void EndGamePanelOpen()
    {
        IngameUI.Instance.FloorFailed();
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
        currentPlayer = (currentPlayer + 1) % (spawnList.spawnList.Length);
        Vector2 currLocation = p1.transform.position;
        Destroy(p1);
        GameObject spawnEffect = Instantiate(respawn, currLocation, Quaternion.identity);
        GameObject spawnPlayer = Instantiate(spawnList.spawnList[currentPlayer], currLocation, Quaternion.identity);
        p1 = spawnPlayer;
        CameraFollowPlayer.Instance.player = p1.transform;
        PlayerInitialized();
    }

    public void ChangeBackPlayer()
    {
        currentPlayer = currentPlayer - 1;
        if (currentPlayer < 0)
        {
            currentPlayer += spawnList.spawnList.Length;
        }
        Debug.Log(currentPlayer);
        Vector2 currLocation = p1.transform.position;
        Destroy(p1);
        GameObject spawnEffect = Instantiate(respawn, currLocation, Quaternion.identity);
        GameObject spawnPlayer = Instantiate(spawnList.spawnList[currentPlayer], currLocation, Quaternion.identity);
        p1 = spawnPlayer;
        CameraFollowPlayer.Instance.player = p1.transform;
        PlayerInitialized();
    }

    //manage player
    public void PlayerInitialized()
    {
        health = maxHealth;
        healthBarSlider.fillAmount = CaculateHealthPercentage();
        mana = maxMana;
        KnockEnemies(p1.transform.position);
        SetAvatar();
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
        skillSlider[0].fillAmount = 0;
        skillSlider[1].fillAmount = 0;
        skillSlider[2].fillAmount = 0;
        skills = new GameObject[] { null, null, null };
        isRecharge = new bool[] { false,false,false };
        avatarList[1].GetComponent<Image>().sprite = null;
        avatarList[2].GetComponent<Image>().sprite = null;
        avatarList[3].GetComponent<Image>().sprite = null;
        int count = 0;
        avatarList[0].GetComponent<Image>().sprite = imageSource.spriteList[currentPlayer];
        count++;
        if (count < lives)
        {
            if (coroutines[0] != null)
            {
                StopCoroutine(coroutines[0]);
            }
            int index1 = currentPlayer - 1;
            if (index1 < 0) index1 += 4;
            avatarList[1].GetComponent<Image>().sprite = imageSource.spriteList[index1];
            skills[0] = skillList.spawnList[index1];
            count++;
            if (count < lives)
            {
                if (coroutines[1] != null)
                {
                    StopCoroutine(coroutines[1]);
                }
                int index2 = currentPlayer - 2;
                if (index2 < 0) index2 += 4;
                avatarList[2].GetComponent<Image>().sprite = imageSource.spriteList[index2];
                skills[1] = skillList.spawnList[index2];
                count++;
                if(count < lives)
                {
                    if (coroutines[2] != null)
                    {
                        StopCoroutine(coroutines[2]);
                    }
                    int index3 = currentPlayer - 3;
                    if (index3 < 0) index3 += 4;
                    avatarList[3].GetComponent<Image>().sprite = imageSource.spriteList[index3];
                    skills[2] = skillList.spawnList[index3];
                }
            }
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

    public void rechargeSkill(int target, float rechargeTime)
    {
        coroutines[target] = StartCoroutine(SkillRecharge(target,rechargeTime));
    }

    public IEnumerator SkillRecharge(int target,float rechargeTime)
    {
        float elapsedTime = 0;
        while (elapsedTime <= rechargeTime)
        {
            elapsedTime += Time.deltaTime;
            skillSlider[target].fillAmount = (1f - elapsedTime / rechargeTime);
            yield return null;
        }
        skillSlider[target].fillAmount = 0;
        isRecharge[target] = false;
    }

    private float CaculateManaPercentage()
    {
        return (mana / maxMana);
    }

    private float CaculateHealthPercentage()
    {
        return (health / maxHealth);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Skill : MonoBehaviour
{
    void Start()
    {
        Invoke("RecoverPlayerHP",0.5f);
    }

    public void RecoverPlayerHP()
    {
        PlayerStats.Instance.HealCharacter(1000);
        GameObject.Destroy(gameObject,1f);
    }
}

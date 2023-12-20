using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{
    public enum ObjectType {COIN,GEM,HEART};
    public ObjectType currentType;
    public int amount;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ( collision.CompareTag("Player") && (gameObject.GetComponent<Slowdown>().pickable==true) )
        {
            if (currentType==ObjectType.COIN)
            {
                PlayerStats.Instance.coin += amount;
            } else if (currentType==ObjectType.GEM) {
            }
            else
            {
                PlayerStats.Instance.HealCharacter(100);
            }
            gameObject.SetActive(false);
        }
    }
}

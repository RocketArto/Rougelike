using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomTrigger : MonoBehaviour
{
    public GameObject monsterGroup;

    void OnTriggerEnter2D()
    {
        monsterGroup.SetActive(true);
    }
}

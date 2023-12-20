using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAttract : MonoBehaviour
{
    public float detectionRange;
    public float coinFlySpeed;

    private void Update()
    {
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

        foreach (GameObject pickup in pickups)
        {
            float distanceToCoin = Vector2.Distance(transform.position, pickup.transform.position);
            if ((distanceToCoin < detectionRange) && (pickup.GetComponent<Slowdown>().pickable==true))
            {
                FlyTowardsPlayer(pickup.transform);
            }
        }
    }

    void FlyTowardsPlayer(Transform coinTransform)
    {
        Vector2 direction = transform.position - coinTransform.position;
        direction.Normalize();
        coinTransform.Translate(direction * coinFlySpeed * Time.deltaTime);
    }
}

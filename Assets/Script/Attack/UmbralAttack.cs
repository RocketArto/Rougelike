using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbralAttack : MonoBehaviour
{
    public GameObject leftUmbral;
    public GameObject leftUmbralPos;

    public GameObject rightUmbral;
    public GameObject rightUmbralPos;

    public float cooldownTime;

    private bool isOnCooldown = false;

    public int checkPlayerDir = 4;

    void Update()
    {
        checkPlayerDir = gameObject.GetComponent<PlayerMovement>().face;
        

        if (!isOnCooldown && Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        StartCooldown();
        if (checkPlayerDir == 1)
        {
            GameObject umbral = Instantiate(rightUmbral, rightUmbralPos.transform.position, Quaternion.identity);
            //direction = Vector2.up;
        }
        else if (checkPlayerDir == 2)
        {
            GameObject umbral = Instantiate(leftUmbral, leftUmbralPos.transform.position, Quaternion.identity);
        }
        else if (checkPlayerDir == 3)
        {
            GameObject umbral = Instantiate(leftUmbral, leftUmbralPos.transform.position, Quaternion.identity);
        }
        else if (checkPlayerDir == 4)
        {
            GameObject umbral = Instantiate(rightUmbral, rightUmbralPos.transform.position, Quaternion.identity);
        }

    }

    void StartCooldown()
    {
        isOnCooldown = true;
        Invoke("ResetCooldown", cooldownTime);
    }

    void ResetCooldown()
    {
        isOnCooldown = false;
    }
}

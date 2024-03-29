using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public static CameraFollowPlayer Instance;
    public Transform player;
    public float smoothing = 0.05f;
    public Vector3 offset;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }


    void FixedUpdate(){
        if (player != null){
            Vector3 newPosition = Vector3.Lerp(transform.position, player.transform.position + offset, smoothing);
            transform.position = newPosition;
        } 
    }

    IEnumerator Wait() { 
        yield return new WaitForSeconds(0.5f);
    }
}

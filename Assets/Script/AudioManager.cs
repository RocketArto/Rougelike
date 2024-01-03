using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip introAudio;
    public AudioClip loopAudio;
    // Start is called before the first frame update
    void Start()
    {
        audioS.loop = true;
        StartCoroutine(changeToLoop());
    }

    // Update is called once per frame
    IEnumerator changeToLoop()
    {
        audioS.clip = introAudio;
        audioS.Play();
        yield return new WaitForSeconds(audioS.clip.length);
        audioS.clip = loopAudio;
        audioS.Play();
    }
}

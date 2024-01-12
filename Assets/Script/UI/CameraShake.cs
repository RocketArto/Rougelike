using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    public AnimationCurve camShakeY;
    public AnimationCurve camShakeX;
    public AnimationCurve camShakeZ;
    public float multiplier = 1f;
    public bool randomize;

    private void Awake()
    {
        Instance = this;
    }

    public void Shake(float intensity, float time)
    {
        StartCoroutine(DoShake(intensity, time));
    }

    IEnumerator DoShake(float scale, float time)
    {
        Vector3 rand = new Vector3(getRandomValue(), getRandomValue(), getRandomValue());
        scale *= multiplier;

        float t = 0;
        while (t < time)
        {
            if (randomize)
            {
                transform.position += new Vector3(camShakeX.Evaluate(t) * scale * rand.x, camShakeY.Evaluate(t) * scale * rand.y, camShakeZ.Evaluate(t) * scale * rand.z);
            }
            else
            {
                transform.position += new Vector3(camShakeX.Evaluate(t) * scale, camShakeY.Evaluate(t) * scale, camShakeZ.Evaluate(t) * scale);
            }

            t += Time.deltaTime / time;
            yield return null;
        }
        //transform.localPosition = Vector3.zero;
    }

    int getRandomValue()
    {
        int[] i = { -1, 1 };
        return i[Random.Range(0, 2)];
    }
}
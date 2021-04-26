using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    SpriteRenderer sp;

    public float blinkFrequency = 0.5f;

    float timer = 0f;

    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > blinkFrequency)
        {
            sp.enabled = !sp.enabled;
            timer = 0f;
        }
    }
}

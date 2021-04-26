using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightbulb : MonoBehaviour
{
    public Sprite unlitLight;
    public Sprite litLight;

    public float minTime;
    public float maxTime;

    bool isLit = false;
    float timer = 0f;
    SpriteRenderer sp;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0f)
        {
            isLit = !isLit;

            sp.sprite = isLit ? litLight : unlitLight;
            timer = Random.Range(minTime, maxTime);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLight : MonoBehaviour
{
    public Sprite unlitSprite;
    public Sprite litSprite;

    public float blinkFrequency;
    public AudioSource alarmSound;
    

    SpriteRenderer sp;
    bool warning = false;
    bool lit = false;
    float timer;
    int warningCount = 0;

    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(warning)
        {
            timer -= Time.deltaTime;
            if(timer < 0f)
            {
                sp.sprite = lit ? unlitSprite : litSprite;
                lit = !lit;
                timer = blinkFrequency;
            }
        }
        
    }

    [ContextMenu("StartWarning")]
    public void StartWarning()
    {
        warning = true;
        warningCount++;
        //timer = blinkFrequency;
        alarmSound.Play();
    }

    [ContextMenu("EndWarning")]
    public void EndWarning()
    {
        warningCount--;

        if(warningCount == 0)
        {
            warning = false;
            lit = false;
            sp.sprite = unlitSprite;
            alarmSound.Stop();

        }
    }


}

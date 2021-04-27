using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{

    public List<SpriteRenderer> healthBars;
    public float blinkLength;
    public float blinkFrequency;

    public SpriteRenderer window;

    public Sprite damagedWindow;
    public Sprite destroyedWindow;

    int health = 4;

    
    [ContextMenu("Reduce health by 1")]
    public void ReduceHealth()
    {
        if(health > 0) {
            health--;
            StartCoroutine(BlinkCoroutine(health));
        }
                       
        if(health == 2) window.sprite = damagedWindow;
        if(health == 0) window.sprite = destroyedWindow;
    }

    IEnumerator BlinkCoroutine(int id)
    {
        float timeSpentBlinking =  0f;
        while(timeSpentBlinking < blinkLength)
        {
            healthBars[id].enabled = !healthBars[id].enabled;
            timeSpentBlinking += blinkFrequency;
            yield return new WaitForSecondsRealtime(blinkFrequency);
        }
        healthBars[id].enabled = false;
    }

    public int GetHealth()
    {
        return health;
    }
}

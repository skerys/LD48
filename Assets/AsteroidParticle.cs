using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidParticle : MonoBehaviour
{
    public Color litColor;
    public Color baseColor;
    public float dimmingSpeed;


    SpriteRenderer sp;
    float litFactor;


    void Update()
    {
        sp = GetComponent<SpriteRenderer>();

        sp.color = Color.Lerp(baseColor, litColor, litFactor);
        if(litFactor > 0f)
        {
            litFactor -= Time.deltaTime * dimmingSpeed;
        }
    }


    public void LightUp()
    {
        litFactor = 1.0f;
        Destroy(this.gameObject, 1.0f / dimmingSpeed);
    }
}

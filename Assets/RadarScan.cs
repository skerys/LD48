using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarScan : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void Update()
    {
        transform.Rotate(0,0, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var asteroid = other.gameObject.GetComponent<Asteroid>();
        if(asteroid)
        {
            asteroid.SpawnParticle();
        }
    }
}



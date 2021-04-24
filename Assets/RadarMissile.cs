using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarMissile : MonoBehaviour
{
    public float flySpeed;
    public ParticleSystem chunksSystem;
    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        body.velocity = transform.up * flySpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        var asteroid = other.GetComponent<Asteroid>();

        if(asteroid)
        {
            asteroid.SpawnCross();
            Destroy(asteroid);

            chunksSystem.Play();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarMissile : MonoBehaviour
{
    public float flySpeed;
    public ParticleSystem chunksSystem;
    Rigidbody2D body;
    public ScreenShakeManager screenShake;

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
            screenShake.AddTrauma(0.5f);

            asteroid.SpawnCross();
            Destroy(asteroid.gameObject);

            chunksSystem.Play();
            Destroy(gameObject);

        }
    }


}

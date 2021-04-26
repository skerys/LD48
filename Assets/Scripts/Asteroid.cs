using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public AsteroidParticle asteroidParticle;
    public AsteroidParticle crossParticle;

    public ShipHealth shipHealth;
    public ScreenShakeManager screenShake;

    public AudioSource deathSound;

    public void SpawnParticle()
    {
        var particle = Instantiate(asteroidParticle, transform.position, Quaternion.identity);
        particle.LightUp();
    }

    public void SpawnCross()
    {
        var particle = Instantiate(crossParticle, transform.position, Quaternion.identity);
        particle.LightUp();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Lander")
        {
            shipHealth.ReduceHealth();
            screenShake.AddTrauma(1.0f);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        AsteroidSpawner.asteroidCount--;
        deathSound.Play();
    }
}

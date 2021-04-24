using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public AsteroidParticle asteroidParticle;
    public AsteroidParticle crossParticle;


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
}

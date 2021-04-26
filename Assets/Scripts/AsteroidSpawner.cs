using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroid;
    public Transform radarLander;
    public float spawnDistance;
    public float asteroidSpeed;
    public ScreenShakeManager screenShake;
    public ShipHealth shipHealth;
    public WarningLight warning;
    public static int asteroidCount = 0;
    int previousAsteroidCount;

    public AudioSource asteroidDeathSound;
    
    [ContextMenu("SpawnRandomAsteroid")]
    public void SpawnAsteroid()
    {
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        Vector3 randomPos = new Vector3(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle), 0f);

        Vector3 dir = -randomPos.normalized;
        Debug.Log(dir);

        var spawnedAsteroid = Instantiate(asteroid, radarLander.position + randomPos * spawnDistance, Quaternion.identity);
        spawnedAsteroid.GetComponent<Rigidbody2D>().velocity = dir * asteroidSpeed;

        spawnedAsteroid.shipHealth = shipHealth;
        spawnedAsteroid.screenShake = screenShake;
        spawnedAsteroid.deathSound = asteroidDeathSound;
        
        if(asteroidCount == 0)
        {
            warning.StartWarning();
        }
        asteroidCount++;

    }

    void Update()
    {
        if(asteroidCount == 0 && previousAsteroidCount > 0)
        {
            warning.EndWarning();
        }
        previousAsteroidCount = asteroidCount;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(radarLander.position, spawnDistance);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroid;
    public Transform radarLander;
    public float spawnDistance;
    public float asteroidSpeed;
    
    [ContextMenu("SpawnRandomAsteroid")]
    public void SpawnAsteroid()
    {
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        Vector3 randomPos = new Vector3(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle));

        Vector3 dir = -randomPos.normalized;

        var spawnedAsteroid = Instantiate(asteroid, radarLander.position + randomPos * spawnDistance, Quaternion.identity);
        spawnedAsteroid.GetComponent<Rigidbody2D>().velocity = dir * asteroidSpeed;

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(radarLander.position, spawnDistance);
    }



}

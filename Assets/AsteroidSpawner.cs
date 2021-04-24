using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public BoxCollider2D insideScreenCollider;
    public GameObject asteroidPrefab;

    Vector2 randomPointInsideScreen()
    {
        Bounds bounds = insideScreenCollider.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(randomX, randomY);
    }



}

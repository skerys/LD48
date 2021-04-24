using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAngle : MonoBehaviour
{
    public float shipAngle;
    float randomSeed;

    void Awake()
    {
        shipAngle = 0f;
        randomSeed = Random.Range(-100f, 100f);
    }

    void Update()
    {
        float shipAngleOffset = Mathf.PerlinNoise(randomSeed, Time.time);

        //scale to (-1;1)
        shipAngleOffset = shipAngleOffset * 2f - 1f;

        shipAngle += shipAngleOffset * Time.deltaTime;
    }
}

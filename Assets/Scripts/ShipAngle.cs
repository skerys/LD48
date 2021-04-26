using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAngle : MonoBehaviour
{
    public float shipAngle;
    public float noiseStrength = 1f;
    public float maxShipAngle = 0.9f;
    public float shipAngleToHealthDrop = 0.8f;
    public float timeUntilHealthDrop = 1f;

    float healthDropTimer;
    bool healthDropping = false;
    float randomSeed;

    public bool stopTilt = false;

    public Transform outsideCamera;
    public ShipHealth health;
    public WarningLight warning;
    public ScreenShakeManager screenShake;

    void Awake()
    {
        shipAngle = 0f;
        randomSeed = Random.Range(-100f, 100f);
    }

    void Update()
    {
        if(stopTilt) return;

        float shipAngleOffset = Mathf.PerlinNoise(randomSeed, Time.time);

        //scale to (-1;1)
        shipAngleOffset = shipAngleOffset * 2f - 1f;
        shipAngleOffset *= noiseStrength;

        shipAngle += shipAngleOffset * Time.deltaTime;
        shipAngle = Mathf.Clamp(shipAngle, -maxShipAngle, maxShipAngle);


        outsideCamera.rotation = Quaternion.Euler(0f, 0f, shipAngle * 15f);

        bool angleInWarningRange = shipAngle > shipAngleToHealthDrop || shipAngle < -shipAngleToHealthDrop;

        if(healthDropping)
        {
            if(!angleInWarningRange)
            {
                warning.EndWarning();
                healthDropping = false;
                healthDropTimer = 0f;
            }

            healthDropTimer += Time.deltaTime;
            if(healthDropTimer > timeUntilHealthDrop)
            {
                health.ReduceHealth();
                screenShake.AddTrauma(0.5f);
                shipAngle = 0f;
                healthDropTimer = 0f;
            }
        }
        else if(angleInWarningRange)
        {
            healthDropping = true;
            warning.StartWarning();
        }

        
    }
}

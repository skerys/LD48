using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInterface : MonoBehaviour
{
    public Transform levelIndicator;
    public float maxIndicator;
    public ShipAngle shipAngle;

    Vector3 initialPosition;

    void Awake()
    {
        initialPosition = levelIndicator.position;
    }

    void Update()
    {
        float clampledShipAngle = Mathf.Clamp(shipAngle.shipAngle, -maxIndicator, maxIndicator);
        levelIndicator.position = new Vector3(initialPosition.x + clampledShipAngle, initialPosition.y);
    }
}

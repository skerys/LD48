using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInterface : MonoBehaviour
{
    public Transform levelIndicator;
    public ShipAngle shipAngle;

    Vector3 initialPosition;

    void Awake()
    {
        initialPosition = levelIndicator.position;
    }

    void Update()
    {
        levelIndicator.position = new Vector3(initialPosition.x + shipAngle.shipAngle, initialPosition.y);
    }
}

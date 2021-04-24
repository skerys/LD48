using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
    public float tiltStrength;

    ShipAngle angle;

    void Awake()
    {
        angle = GetComponent<ShipAngle>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)) Tilt(-tiltStrength * Time.deltaTime);
        if(Input.GetKey(KeyCode.RightArrow)) Tilt(tiltStrength * Time.deltaTime);
        
    }

    void Tilt(float x)
    {
        angle.shipAngle += x;
    }
}

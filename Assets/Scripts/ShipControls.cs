using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
    public float tiltStrength;

    public Sprite joystickNeutral;
    public Sprite joystickLeft;
    public Sprite joystickRight;
    public Sprite joystickUp;
    public Sprite joystickDown;

    public SpriteRenderer joystick;

    ShipAngle angle;

    void Awake()
    {
        angle = GetComponent<ShipAngle>();
    }

    void Update()
    {
        Tilt(tiltStrength * Time.deltaTime);

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) { 
            joystick.sprite = joystickLeft;
        }
        else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
            joystick.sprite = joystickRight;
        }
        else if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
            joystick.sprite = joystickUp;
        }
        else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
            joystick.sprite = joystickDown;
        }
        else
        {
            joystick.sprite = joystickNeutral;
        }
        
    }

    void Tilt(float x)
    {
        angle.shipAngle += x;
    }
}

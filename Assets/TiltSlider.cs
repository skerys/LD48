using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltSlider : MonoBehaviour
{
    Vector2 screenPoint;
    Vector2 offset;
    public ShipControls ship;

    void Awake()
    {
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.localPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        Vector2 cursorPoint = Input.mousePosition;
        Vector2 cursorPosition = (Vector2)Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.localPosition = new Vector3(Mathf.Clamp(cursorPosition.x, -0.8125f, 0.8125f), 0f, 0f);

        ship.tiltStrength = transform.localPosition.x / 0.8125f;
    }
}

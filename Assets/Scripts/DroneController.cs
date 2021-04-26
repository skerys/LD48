using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public float reticleSpeed;

    public Button fixButton;
    Rigidbody2D body;

    GameObject hoverHole;


    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        fixButton.OnButtonPress += FixHole;
    }

    void OnDisable()
    {
        fixButton.OnButtonPress -= FixHole;
    }

    void Update()
    {
        float dirX = 0;
        float dirY = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(dirX, dirY);

        dir = dir.normalized;
        Move(dir);

    }

    void FixHole()
    {
        if(hoverHole) hoverHole.SetActive(false);
    }

    void Move(Vector2 dir)
    {
        body.velocity = dir * reticleSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Hole")
        {
            hoverHole = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(hoverHole)
            hoverHole = null;
    }
}

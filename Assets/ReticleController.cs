using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public float reticleSpeed;

    Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(dirX, dirY);

        dir = dir.normalized;
        Move(dir);
    }

    void Move(Vector2 dir)
    {
        body.velocity = dir * reticleSpeed;
    }
}

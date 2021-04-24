using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public float reticleSpeed;
    public RadarMissile missile;
    public Transform radarShip;
    public ParticleSystem chunkSystem;

    Rigidbody2D body;

    float reloadTimer = 0f;

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

        reloadTimer -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) && reloadTimer < 0f)
        {
            Vector3 targetPos = transform.position;
            targetPos.z = radarShip.position.z;

            Vector3 shootDir = targetPos - radarShip.position;
            Vector3 rotatedDir = Quaternion.Euler(0, 0, 0) * shootDir;

            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, rotatedDir);
            var newMissile = Instantiate(missile, radarShip.position, rotation);
            newMissile.chunksSystem = chunkSystem;

            reloadTimer = 1f;

            Destroy(newMissile.gameObject, 3f);
        }
    }

    void Move(Vector2 dir)
    {
        body.velocity = dir * reticleSpeed;
    }
}

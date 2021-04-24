using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPanorama : MonoBehaviour
{
    float panoramaWidthWorldSpace;
    public Transform drones;

    public float wobbleAmplitude;
    public float wobbleFrequency;

    void Start()
    {
        panoramaWidthWorldSpace = GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.x;
        Debug.Log(panoramaWidthWorldSpace);

    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        
        transform.Translate(inputX * Vector3.right * Time.deltaTime * 5f);
        
        if(transform.position.x > panoramaWidthWorldSpace)
        {
            transform.Translate(-panoramaWidthWorldSpace * Vector3.right);
        }

        if(transform.position.x < -panoramaWidthWorldSpace)
        {
            transform.Translate(panoramaWidthWorldSpace * Vector3.right);
        }

        drones.position = new Vector3(-transform.position.x, wobbleAmplitude * Mathf.Sin(wobbleFrequency * Time.time), drones.position.z);
        transform.position = new Vector3(transform.position.x, wobbleAmplitude * Mathf.Sin(wobbleFrequency * Time.time), transform.position.z);
    }
}

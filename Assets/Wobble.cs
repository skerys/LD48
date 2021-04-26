using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    public float wobbleAmount;
    public float wobbleFrequency;
   

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + Mathf.Sin(Time.time * wobbleFrequency) * wobbleAmount * Time.deltaTime);
    }
}

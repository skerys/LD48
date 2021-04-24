using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public float progressPercentagePerSecond = 1f;
    public SpriteRenderer exteriorBackground;
    float progressValue = 0f;

    float backgroundHeight;
    float bgInitialY;

    void Start()
    {
        backgroundHeight = exteriorBackground.sprite.bounds.extents.y * 2f;
        bgInitialY = exteriorBackground.transform.position.y;
    }

    void Update()
    {
        progressValue += progressPercentagePerSecond / 100f * Time.deltaTime;

        float newY = Mathf.Lerp(bgInitialY, bgInitialY + backgroundHeight, progressValue);

        exteriorBackground.transform.position = new Vector2(exteriorBackground.transform.position.x, newY);
    }
}

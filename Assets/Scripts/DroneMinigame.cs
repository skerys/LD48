using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMinigame : MonoBehaviour
{
    float panoramaWidthWorldSpace;
    public Transform drones;

    public float wobbleAmplitude;
    public float wobbleFrequency;

    public GameObject holeA;
    public GameObject holeB;
    public GameObject consoleText;
    public GameObject droneGO;
    public Animator extendableButton;

    public Transform oxygenFillBar;
    public float oxygenDecreaseRate;

    float oxygenAmount = 1f;

    float initDroneX;
    float initDroneY;

    float initPanoramaY;

    [HideInInspector]
    public bool gameInProgress =false;

    void Start()
    {
        panoramaWidthWorldSpace = GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.x;
        Debug.Log(panoramaWidthWorldSpace);

        initDroneX = drones.localPosition.x;
        initDroneY = drones.localPosition.y;

        initPanoramaY = transform.localPosition.y;
    }

    void Update()
    {
        //Oxygen Handling
        if(holeA.activeSelf || holeB.activeSelf)
        {
            if(oxygenAmount > 0f)
            {
                oxygenAmount -= oxygenDecreaseRate * Time.deltaTime;
                oxygenFillBar.localScale = new Vector3(1f, oxygenAmount, 1f);
            }
            else if(oxygenAmount < 0f)
            {
                oxygenAmount = 0f;
            }
        }
        else
        {
            if(oxygenAmount < 1f)
            {
                oxygenAmount += oxygenDecreaseRate * 3f * Time.deltaTime;
                oxygenFillBar.localScale = new Vector3(1f, oxygenAmount, 1f);
            }
            else if(oxygenAmount > 1f)
            {
                oxygenAmount = 1f;
            }
        }



        //---PANORAMA + DRONE HANDLING
        float inputX = -Input.GetAxisRaw("Horizontal");
        
        transform.Translate(inputX * Vector3.right * Time.deltaTime * 5f);
        
        if(transform.position.x > panoramaWidthWorldSpace / 3f)
        {
            transform.Translate(-panoramaWidthWorldSpace * Vector3.right);
        }

        if(transform.position.x < -panoramaWidthWorldSpace / 3f)
        {
            transform.Translate(panoramaWidthWorldSpace * Vector3.right);
        }

        drones.localPosition = new Vector3(-transform.localPosition.x + initDroneX, wobbleAmplitude * Mathf.Sin(wobbleFrequency * Time.time) + initDroneY, drones.localPosition.z);
        transform.localPosition = new Vector3(transform.localPosition.x, wobbleAmplitude * Mathf.Sin(wobbleFrequency * Time.time) + initPanoramaY, transform.localPosition.z);
    }

    public void EnableHoleA()
    {
        holeA.SetActive(true);
    }

    public void EnableHoleB()
    {
        holeB.SetActive(true);
    }

    public void StartDroneGame()
    {
        gameInProgress = true;
        consoleText.SetActive(false);
        droneGO.SetActive(true);
        extendableButton.SetTrigger("ExtendFix");
    }

    public void EndDroneGame()
    {
        gameInProgress = false;
        consoleText.SetActive(true);
        droneGO.SetActive(false);
        extendableButton.SetTrigger("RetractFix");
    }

    public bool CheckIfCanEnd()
    {
        return oxygenAmount > 0.999f && !holeA.activeSelf && !holeB.activeSelf;
    }
}

using System;
using UnityEngine;

public enum SwitchState{
    Console,
    Weapons
}

public class Switch : MonoBehaviour
{
    public Sprite spriteWeapons;
    public GameObject objectWeapons;
    public GameObject infoInConsole;
    public Sprite spriteConsole;
    public GameObject objectConsole;
    public ExtendableButton extendableButton;

    public DroneMinigame drones;
    public AudioSource sound;

    public event Action OnSwitchChange = delegate{};


    public SwitchState switchState = SwitchState.Console;

    SpriteRenderer sp;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public void ChangeState()
    {
        OnSwitchChange();

        sp.sprite = switchState == SwitchState.Console ? spriteConsole : spriteWeapons;

        if(switchState == SwitchState.Console)
        {
            infoInConsole.SetActive(false);
            if(!objectConsole.activeSelf) objectConsole.SetActive(true);
            if(objectWeapons.activeSelf) objectWeapons.SetActive(false);
            extendableButton.RetractShootButton();
            if(drones.gameInProgress) extendableButton.ExtendFixButton();

        }
        else if(switchState == SwitchState.Weapons)
        {
            if(objectConsole.activeSelf) objectConsole.SetActive(false);
            if(!objectWeapons.activeSelf) objectWeapons.SetActive(true);
            if(drones.gameInProgress) extendableButton.RetractFixButton();
            extendableButton.ExtendShootButton();
        }
    }

    void OnMouseDown()
    {
        switchState = switchState == SwitchState.Console ? SwitchState.Weapons : SwitchState.Console;

        ChangeState();
        sound.Play();
    }


}

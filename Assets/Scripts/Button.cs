using System;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Sprite unpressedSprite;
    public Sprite pressedSprite;
    public AudioSource buttonClick;
    public AudioSource buttonRelease;

    public event Action OnButtonPress = delegate{};

    SpriteRenderer sp;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        sp.sprite = pressedSprite;
        buttonClick.Play();
        OnButtonPress();
    }

    void OnMouseUp()
    {
        sp.sprite = unpressedSprite;
        buttonRelease.Play();
    }

    void OnMouseExit()
    {
        sp.sprite = unpressedSprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendableButton : MonoBehaviour
{
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ExtendShootButton()
    {
        animator.SetBool("Shoot", true);
    }

    public void RetractShootButton()
    {
        animator.SetBool("Shoot", false);
    }

    public void ExtendFixButton()
    {
        animator.SetBool("Fix", true);
    }

    public void RetractFixButton()
    {
        animator.SetBool("Fix", false);
    }
}

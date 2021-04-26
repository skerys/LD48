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
        animator.SetTrigger("ExtendShoot");
    }

    public void RetractShootButton()
    {
        animator.SetTrigger("RetractShoot");
    }

    public void ExtendFixButton()
    {
        animator.SetTrigger("ExtendFix");
    }

    public void RetractFixButton()
    {
        animator.SetTrigger("RetractFix");
    }
}

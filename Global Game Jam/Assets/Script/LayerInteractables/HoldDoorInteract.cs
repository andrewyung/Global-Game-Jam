using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldDoorInteract : ButtonInteractable
{
    private BoxCollider boxCollider;

    private Animator[] animator;
    // Use this for initialization
    void Start()
    {
        boxCollider = GetComponentInChildren<BoxCollider>();
        animator = GetComponentsInChildren<Animator>();
    }

    public override void doAction()
    {
        boxCollider.enabled = false;

        for (int i = 0; i < animator.Length; i++)
        {
            animator[i].SetBool("Open", true);
        }
    }

    public override void undoAction()
    {
        boxCollider.enabled = true;

        for (int i = 0; i < animator.Length; i++)
        {
            animator[i].SetBool("Open", false);
        }
    }
}


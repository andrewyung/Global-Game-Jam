using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDoorInteract : ButtonInteractable {

    public float timer = 2;

    private BoxCollider boxCollider;

    private Animator[] animator;

    void Start ()
    {
        boxCollider = GetComponentInChildren<BoxCollider>();
        animator = GetComponentsInChildren<Animator>();
    }

    public override void doAction()
    {
        StopAllCoroutines();
        StartCoroutine(timerDoor());
    }

    IEnumerator timerDoor()
    {
        for (int  i = 0; i < animator.Length; i++)
        {
            animator[i].SetBool("Open", true);
        }
        boxCollider.enabled = (false);

        yield return new WaitForSeconds(timer);

        for (int i = 0; i < animator.Length; i++)
        {
            animator[i].SetBool("Open", false);
        }
        boxCollider.enabled = (true);
    }
}

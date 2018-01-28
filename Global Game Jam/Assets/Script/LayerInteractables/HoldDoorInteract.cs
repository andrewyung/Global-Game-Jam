using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldDoorInteract : ButtonInteractable
{
    private GameObject doorChild;
    // Use this for initialization
    void Start()
    {
        doorChild = transform.GetChild(0).gameObject;
    }

    public override void doAction()
    {
        doorChild.gameObject.SetActive(false);
    }

    public override void undoAction()
    {
        doorChild.gameObject.SetActive(true);
    }
}


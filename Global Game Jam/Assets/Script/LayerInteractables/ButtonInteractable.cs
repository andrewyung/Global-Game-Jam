using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : MonoBehaviour
{
    public virtual void doAction()
    {
        Debug.Log("No doAction");
    }
    public virtual void undoAction()
    {
        Debug.Log("No undoAction");
    }
}

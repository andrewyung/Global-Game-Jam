using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDoorInteract : ButtonInteractable {

    public float timer = 2;

    private GameObject doorChild;
	// Use this for initialization
	void Start () {
        doorChild = transform.GetChild(0).gameObject;
	}

    public override void doAction()
    {
        StopAllCoroutines();
        StartCoroutine(timerDoor());
    }

    IEnumerator timerDoor()
    {
        doorChild.SetActive(false);

        yield return new WaitForSeconds(timer);

        doorChild.SetActive(true);
    }
}

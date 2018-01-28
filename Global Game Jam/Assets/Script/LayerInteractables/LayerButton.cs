using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerButton : MonoBehaviour {

    [SerializeField]
    private ButtonInteractable targetInteractable;

	// Use this for initialization
	void Start () {
		
	}
	

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player") && targetInteractable != null)
        {
            Debug.Log("interact");
            targetInteractable.doAction();
        }
    }
}

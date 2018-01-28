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
        if ((collider.gameObject.tag.Equals("SuitPlayer") ||
            collider.gameObject.tag.Equals("Boulder"))
            && targetInteractable != null)
        {
            Debug.Log("interact");
            targetInteractable.doAction();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if ((collider.gameObject.tag.Equals("SuitPlayer") ||
            collider.gameObject.tag.Equals("Boulder"))
            && targetInteractable != null)
        {
            Debug.Log("interact");
            targetInteractable.undoAction();
        }
    }
}

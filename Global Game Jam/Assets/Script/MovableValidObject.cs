using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableValidObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Valid Area"))
        {
            Debug.Log("other ; " + collider.name);
            handleDestroy(); 
        }
    }

    public virtual void handleDestroy()
    {
        Debug.Log("Do something to destroy");
    }
}

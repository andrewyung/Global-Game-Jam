using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerHole : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Boulder"))
        {
            collider.gameObject.layer = 17;//layer 17 is LayerTraverse
            //collider.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag.Equals("Boulder"))
        {
            collider.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, collider.gameObject.GetComponent<Rigidbody>().velocity.y, 0);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag.Equals("Boulder"))
        {
            collider.gameObject.layer = 12;//back to default
            collider.gameObject.GetComponent<Renderer>().enabled = false;
            collider.gameObject.GetComponentsInChildren<Renderer>()[1].enabled = false;

            collider.gameObject.transform.SetParent(LayerManager.getNextLayerGameObject().transform);
            collider.gameObject.transform.localScale = new Vector3(7, 7, 1);
        }
    }
}

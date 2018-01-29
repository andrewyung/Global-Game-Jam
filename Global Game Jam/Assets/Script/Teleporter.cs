using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    [SerializeField]
    private ParticleSystem particleSysIn;
    [SerializeField]
    private ParticleSystem particleSysOut;

    [SerializeField]
    private Teleporter target;

    private bool ready = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Boulder") && ready)
        {
            Vector3 positionOffset = collider.transform.position - transform.position;

            collider.transform.position = target.transform.position + positionOffset;
            collider.transform.SetParent(target.transform.parent.transform, true);

            particleSysOut.Play();
            target.particleSysIn.Play();

            target.ready = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        ready = true;
    }
}

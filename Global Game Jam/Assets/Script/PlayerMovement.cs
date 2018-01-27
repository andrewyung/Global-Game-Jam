using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField]
    private float movementMultiplier = 250;

    [SerializeField]
    private float maxVelocityMagnitude = 25;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	public void moveDirection(Vector3 relativeDirection)
    {
        rb.velocity = Vector3.ClampMagnitude(relativeDirection.normalized * movementMultiplier, maxVelocityMagnitude);
        Debug.Log(rb.velocity);
    }
}

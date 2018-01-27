using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private PlayerMovement playerMovement;

    private Vector3 movementVector;

	// Use this for initialization
	void Start () {
        playerMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        movementVector.Set(0, 0, 0);
		if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.A))
            {
                movementVector += transform.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movementVector += -transform.right;
            }
            if (Input.GetKey(KeyCode.W))
            {
                movementVector += Quaternion.Euler(0, 0, 23) * -transform.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementVector += Quaternion.Euler(0, 0, 23) * transform.forward;
            }
        }
        
        playerMovement.moveDirection(movementVector);
	}
}

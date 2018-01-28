using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MovableValidObject {

    [SerializeField]
    private PlayerMovement playerMovement;

    private Vector3 movementVector;

    private bool groundContact = false;

	// Use this for initialization
	void Start () {
        playerMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        movementVector.Set(0, 0, 0);
		if (Input.anyKey && !LayerManager.isLayerAnimating())
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
                movementVector += Quaternion.Euler(-CameraOrientate.mainCameraTransform.rotation.eulerAngles.x, 2, 0) * -transform.forward * 2f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementVector += Quaternion.Euler(-CameraOrientate.mainCameraTransform.rotation.eulerAngles.x, 2, 0) * transform.forward * 2f;
            }
            Debug.DrawLine(transform.position, transform.position + Quaternion.Euler(-CameraOrientate.mainCameraTransform.rotation.eulerAngles.x, 2, 0) * transform.forward, Color.black);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LayerManager.nextLayer();
            }
        }
        //Debug.Log(groundContact);
        if (groundContact)
        {
            playerMovement.moveDirection(movementVector);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Layer"))
        {
            groundContact = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag.Equals("Layer"))
        {
            groundContact = false;
        }
    }

    public override void handleDestroy()
    {
        GameManager.endGame();
        //destroy animation
        //end game
    }
}

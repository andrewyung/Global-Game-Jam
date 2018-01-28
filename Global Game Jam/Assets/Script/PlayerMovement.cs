using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MovableValidObject
{

    public string activeTag = "SuitPlayer";

    public static bool groundContact = false;

    private bool activated = false;

    private Rigidbody rb;
    private Animator animator;
    private SpriteRenderer sr;

    private Vector3 lastKeyDirectional;

    [SerializeField]
    private float movementMultiplier = 250;

    [SerializeField]
    private float maxVelocityMagnitude = 25;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void setActivated(bool activated)
    {
        if (activated)
        {
            tag = activeTag;

            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }
        else
        {
            tag = "Untagged";
        }
        Debug.Log(gameObject.name);
        animator.SetBool("activated", activated);
        this.activated = activated;
    }

    public void handleMoveDirection()
    {
        Vector3 keyDirectional = Vector3.zero;
        Vector3 movementVector = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            movementVector += transform.right;

            keyDirectional += -Vector3.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementVector += -transform.right;

            keyDirectional += Vector3.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movementVector += Quaternion.Euler(-CameraOrientate.mainCameraTransform.rotation.eulerAngles.x, 4, 0) * -transform.forward * 2f;

            keyDirectional += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector += Quaternion.Euler(-CameraOrientate.mainCameraTransform.rotation.eulerAngles.x, 4, 0) * transform.forward * 2f;

            keyDirectional += -Vector3.up;
        }

        rb.velocity = Vector3.ClampMagnitude(movementVector.normalized * movementMultiplier, maxVelocityMagnitude);
        //Debug.Log(rb.velocity);

        if (keyDirectional == Vector3.zero)
        {
            animator.SetBool("Running", false);

            checkSpriteFlip(lastKeyDirectional);
        }
        else
        {
            lastKeyDirectional = keyDirectional;
            animator.SetFloat("Horizontal", keyDirectional.x);
            animator.SetFloat("Vertical", keyDirectional.y);

            animator.SetBool("Running", true);

            checkSpriteFlip(-lastKeyDirectional);
        }
    }

    void checkSpriteFlip(Vector3 keyDirection)
    {
        if (keyDirection.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Layer") && activated)
        {
            groundContact = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag.Equals("Layer") && activated)
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

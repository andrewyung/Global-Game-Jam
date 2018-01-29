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

            groundContact = true;
        }
        else
        {
            tag = "Untagged";
        }
        Debug.Log(gameObject.name);
        animator.SetBool("activated", activated);
        this.activated = activated;
    }

    public void Update()
    {
        if (tag.Equals("Player"))
        {
            RaycastHit rch;
            Physics.Raycast(transform.position, -transform.up, out rch, 0.6f);
            Debug.DrawLine(transform.position, transform.position - (transform.up * 0.6f), Color.blue);

            if (rch.collider != null)
            {
                transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {

                transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    public void stopXZMovement()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

    public void handleMoveDirection()
    {
        Debug.Log(2);
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
        if (gameObject.tag.Equals("Rocket"))
        {
            rb.velocity = new Vector3(0, 50, 0);
        }
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

        Debug.Log(3);
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

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == 14 && activated)
        {
            groundContact = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 14 && activated)
        {
            groundContact = false;
        }
    }
    
    public override void handleDestroy()
    {
        Debug.Log("tag : " + gameObject.tag);
        if (gameObject.tag.Equals("Rocket"))
        {
            GameManager.winGame();
        }
        else
        {
            GameManager.endGame();
            //destroy animation
            //end game
        }
    }
}

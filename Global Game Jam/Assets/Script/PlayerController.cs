using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float suitEnterDistance = 0.3f;

    [SerializeField]
    private PlayerMovement[] playerSuitMovement;

    [SerializeField]
    private PlayerMovement defaultPlayerMovement;

    private static PlayerMovement currentPlayerMovement;

    private Vector3 movementVector;
    private static bool layerChangable = true;

	// Use this for initialization
	void Start () {
        defaultPlayerMovement = GetComponentInChildren<PlayerMovement>();

        currentPlayerMovement = defaultPlayerMovement;
        currentPlayerMovement.setActivated(true);
    }
	
	// Update is called once per frame
	void Update () {
        movementVector.Set(0, 0, 0);
		if (Input.anyKey && !LayerManager.isLayerAnimating())
        {
            //Debug.DrawLine(transform.position, transform.position + Quaternion.Euler(-CameraOrientate.mainCameraTransform.rotation.eulerAngles.x, 2, 0) * transform.forward, Color.black);
            if (Input.GetKeyDown(KeyCode.Space) && !LayerManager.isLayerAnimating())
            {
                LayerManager.nextLayer(currentPlayerMovement == defaultPlayerMovement, layerChangable);
                currentPlayerMovement.stopXZMovement();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentPlayerMovement == defaultPlayerMovement)//not in suit, check for suit enter
                {
                    for (int i = 0; i < playerSuitMovement.Length; i++)
                    {
                        Debug.Log("disance from suit: " + Vector3.Distance(playerSuitMovement[i].transform.position, defaultPlayerMovement.gameObject.transform.position));
                        if (Vector3.Distance(playerSuitMovement[i].transform.position, defaultPlayerMovement.gameObject.transform.position) < suitEnterDistance)
                        {
                            //deactivate old
                            currentPlayerMovement.setActivated(false);
                            currentPlayerMovement = playerSuitMovement[i];
                            //activate new
                            currentPlayerMovement.setActivated(true);

                            defaultPlayerMovement.gameObject.SetActive(false);
                            break;
                        }
                    }
                }
                else //in suit
                {
                    defaultPlayerMovement.gameObject.transform.position = -new Vector3(0, 0.1f, 0) + currentPlayerMovement.gameObject.transform.position;
                    //deactivate old
                    currentPlayerMovement.setActivated(false);
                    currentPlayerMovement = defaultPlayerMovement;
                    //activate new
                    currentPlayerMovement.setActivated(true);

                    defaultPlayerMovement.gameObject.SetActive(true);
                }
            }
        }
        
        //Debug.Log(PlayerMovement.groundContact);
        if (PlayerMovement.groundContact)
        {
            //Debug.Log(1);
            currentPlayerMovement.handleMoveDirection();
        }
    }

    public static void setLayerChangable(bool changable, PlayerMovement caller)
    {
        if (currentPlayerMovement == caller)
        {
            layerChangable = changable;
        }
    }
}

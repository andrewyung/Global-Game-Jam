using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraOrientate : MonoBehaviour {

    [SerializeField]
    private Transform cameraTransform;

    public static Transform mainCameraTransform;

	// Use this for initialization
	void Start () {
		if (mainCameraTransform == null)
        {
            if (cameraTransform != null)
            {
                mainCameraTransform = cameraTransform;
            }
            else
            {
                mainCameraTransform = Camera.main.transform;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.forward = -mainCameraTransform.transform.forward;
    }
}

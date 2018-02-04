using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerButton : MonoBehaviour {

    [SerializeField]
    private Sprite pressedSprite;

    [SerializeField]
    private Sprite unpressedSprite;

    [SerializeField]
    private ButtonInteractable targetInteractable;

    private SpriteRenderer sr;

    private LineRenderer lr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
	}

    public void Update()
    {
        if (transform.position.y > 3)
        {
            lr.enabled = true;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, targetInteractable.transform.GetChild(0).GetChild(0).position);
        }
        else
        {
            lr.enabled = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject.tag.Equals("SuitPlayer") ||
            collider.gameObject.tag.Equals("Boulder"))
            && targetInteractable != null)
        {
            Debug.Log("interact");
            sr.sprite = pressedSprite;
            targetInteractable.doAction();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if ((collider.gameObject.tag.Equals("SuitPlayer") ||
            collider.gameObject.tag.Equals("Boulder"))
            && targetInteractable != null)
        {
            Debug.Log("interact");
            sr.sprite = unpressedSprite;
            targetInteractable.undoAction();
        }
    }
}

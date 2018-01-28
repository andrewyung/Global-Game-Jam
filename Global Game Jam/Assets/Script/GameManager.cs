using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private UIManager ui;

    private static GameManager gm;

    void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(this);
        }
    }

	// Use this for initialization
	void Start () {
		if (ui == null)
        {
            ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        }
	}
	
	public static void endGame()
    {
        gm.ui.setText("You died!");
    }

    public static void layerChange(int toLayer)
    {
        gm.ui.setText("Layer " + (toLayer + 1));
    }

}

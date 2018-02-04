using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private UIManager ui;

    private static GameManager gm;

    void Awake()
    {
        gm = this;
    }

	// Use this for initialization
	void Start () {
		if (ui == null)
        {
            ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        }
        gm.ui.setText("Get to your space ship to escape!");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Test");

            startGame();
        }
    }
	
	public static void endGame()
    {
        gm.ui.setText("You died!");
    }
    public static void winGame()
    {
        gm.ui.setText("You won!");
    }
    public static void startGame()
    {
        LayerManager.reset();
    }


    public static void layerChange(int toLayer, bool isOrb, bool validArea)
    {
        if (!isOrb)
        {
            gm.ui.setText("Exit suit to teleport");
            return;
        }
        if (!validArea)
        {
            gm.ui.setText("Not in valid area for layer change");
            return;
        }
        gm.ui.setText("Layer " + (toLayer + 1) % LayerManager.numberOfLayers());
    }

}

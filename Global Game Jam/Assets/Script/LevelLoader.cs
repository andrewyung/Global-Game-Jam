using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    private static int currentLevel = 1;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this.gameObject);		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLevel - 1 > 0)
        {
            SceneManager.LoadScene(currentLevel--);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLevel + 1 <= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentLevel++);
        }
    }
}

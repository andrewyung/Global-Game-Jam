using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    private static int currentLevel = 1;

    private static LevelLoader ll;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this.gameObject);
        if (ll != null)
        {
            Destroy(gameObject);
        }
        else
        {
            ll = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Level" + currentLevel);

            GameManager.startGame();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLevel - 1 > 0)
        {
            SceneManager.LoadSceneAsync("Level" + --currentLevel);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLevel + 1 <= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync("Level" + ++currentLevel);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

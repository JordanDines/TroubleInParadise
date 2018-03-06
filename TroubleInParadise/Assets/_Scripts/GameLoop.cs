using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    public string gameScene = "_GameScene";
    public string menuScene = "_MenuScene";

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
	}

    //goes to the mian game scene
    public void GoToGameScene()
    {
        SceneManager.LoadScene(gameScene);
    }
    
    //ending scene
    public void GoToMenuScene()
    {
        SceneManager.LoadScene(menuScene);
    }

	public void QuitGame () {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}

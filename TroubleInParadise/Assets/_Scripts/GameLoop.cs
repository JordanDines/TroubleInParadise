using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    public string mainScene = "_MainScene";
    public string startScene = "GameScene";
    public string endScene = "EndScene";

    //goes to the mian game scene
    public void GoToGameScene()
    {
        SceneManager.LoadScene(mainScene);
    }
    
    //ending scene
    public void GoToStartScene()
    {
        SceneManager.LoadScene(startScene);
    }

    //start scene
    public void GoToEndScene()
    {
        SceneManager.LoadScene(endScene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Button_Menu : MonoBehaviour
{
    [FormerlySerializedAs("startScene")] public int mainGameScene;
    public int creditScene;
    public int settingScene;
    public int menuScene;
    public int endScenePlayerWon;
    public int endSceneDMWon;
    public void Game()
    {
        SceneManager.LoadScene(mainGameScene);

    }
    public void Credits()
    {
        SceneManager.LoadScene(creditScene);

    }
    public void Settings()
    {
        SceneManager.LoadScene(settingScene,LoadSceneMode.Additive);

    }

    public void Menu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void EndScenePlayerWon()
    {
        SceneManager.LoadScene(endScenePlayerWon);
    }
    public void EndSceneDMWon()
    {
        SceneManager.LoadScene(endSceneDMWon);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void back()
    {
        SceneManager.LoadScene(menuScene);
        
            SceneManager.UnloadSceneAsync("Settings"); 
    }
    
    
}


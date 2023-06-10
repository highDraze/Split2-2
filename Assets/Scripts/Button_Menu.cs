using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Menu : MonoBehaviour
{
    public int startScene;
    public int creditScene;
    public int settingScene;
    public int menuScene;
    public void Game()
    {
        SceneManager.LoadScene(startScene);

    }
    public void Credits()
    {
        SceneManager.LoadScene(creditScene);

    }
    public void Settings()
    {
        SceneManager.LoadScene(creditScene,LoadSceneMode.Additive);

    }

    public void Exit()
    {
        Application.Quit();
    }
    public void back()
    {
        SceneManager.LoadScene(menuScene);
    }
}


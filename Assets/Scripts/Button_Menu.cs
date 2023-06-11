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
    public int pause;

    public AudioSource ingameM;
    public AudioSource menuM;
    private AudioSource men;

    private bool toggled = false;
    public void Game()
    {
        menuM.Stop();
        //ingameM.Play();
        //DontDestroyOnLoad(ingameM);
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
        men= GameObject.Find("Music").GetComponent<AudioSource>();
        men.Play();
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
    public void Update()
    {
        /*
       if(Input.GetButtonDown("Cancel") && toggled==false)
       {
        Time.timeScale = 0;
        toggled = true;
        SceneManager.LoadScene(pause, LoadSceneMode.Additive);
       }
        else if(Input.GetButtonDown("Cancel") && toggled==true)
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("Pause"); 
        }*/
    }
    
}


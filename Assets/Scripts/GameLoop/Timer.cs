using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    [Header("Component")] public TextMeshProUGUI timerText;

    [FormerlySerializedAs("timeRemaining")]
    [FormerlySerializedAs("currentTime")] [Header("Timer Settings")] 
    
    [SerializeField]
    public float gameTime;
    [FormerlySerializedAs("timerIsRunning")] public bool stopTimer = false;
    public Slider Timerslider;
    private Button_Menu buttonMenu;

    private void Start()
    {
        // Starts the timer automatically
        stopTimer = false;
        Timerslider.maxValue = gameTime;
        Timerslider.value = gameTime;
        buttonMenu = GetComponent<Button_Menu>();

    }
    // Update is called once per frame
    void Update()
    {
        
        
        if (!stopTimer)
        {
            if (gameTime > 0)
            {
                gameTime -= Time.deltaTime;
                Timerslider.value = gameTime;
                timerText.text = gameTime.ToString();
                DisplayTime(gameTime);
            }
            else
            {
                stopTimer = true;
                Debug.Log("Time has run out!");
                gameTime = 0;
                //TODO: Screen to Menu
                buttonMenu.EndScene();
            }
        }
        
        
        void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }
}

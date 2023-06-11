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
   
    private float maxGameTime;
    [FormerlySerializedAs("timerIsRunning")] public bool stopTimer = false;

      [Header("Time Params")] 
    [SerializeField] private float _axisOffset;
    [SerializeField] private Gradient _nightLight;
    [SerializeField] private AnimationCurve _sunCurve;
 
    [Header("Objects")]
    [SerializeField] private Light _sun;
    public Slider Timerslider;
    private Button_Menu buttonMenu;

    private void Start()
    {
        maxGameTime = gameTime;
        // Starts the timer automatically
        stopTimer = false;
        Timerslider.maxValue = gameTime;
        Timerslider.value = gameTime+1;
        buttonMenu = GetComponent<Button_Menu>();
        DisplayTime(gameTime);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
                ProgressTime();
                DisplayTime(gameTime);
            }
            else
            {
                stopTimer = true;
                Debug.Log("Time has run out!");
                gameTime = 0;
                buttonMenu.EndSceneDMWon();
            }
        }

    }

     private void ProgressTime()
    {
        float currentTime = (maxGameTime - gameTime) / maxGameTime; 
        float sunRotation = Mathf.Lerp(0, 190, currentTime);
 
        _sun.transform.rotation = Quaternion.Euler(sunRotation, _axisOffset, 0);
 
        RenderSettings.ambientLight = _nightLight.Evaluate(currentTime);
        _sun.intensity = _sunCurve.Evaluate(currentTime); 
    }
}

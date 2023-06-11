using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    public AudioMixer mixer;

    private void Awake(){
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if(musicObj.Length > 1){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol",Mathf.Log10 (sliderValue) *20);

        Debug.Log(Mathf.Log10 (sliderValue) *20);
    }

}

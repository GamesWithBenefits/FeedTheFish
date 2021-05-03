using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Text toggleMusictxt; 
    private void Start()
    {
        if (BgSound.BgInstance.audi.isPlaying)
        {
            toggleMusictxt.text = "Sound       Off";
        }
        else
        {
            toggleMusictxt.text = "Sound       On";
        }
    }
    public void MusicToggle()
    {
        if (BgSound.BgInstance.audi.isPlaying)
        {
            BgSound.BgInstance.audi.Stop();
            toggleMusictxt.text = "Sound       On";
            
        }
        else
        {
            BgSound.BgInstance.audi.Play();
            toggleMusictxt.text = "Sound       Off";
        }
    }
}
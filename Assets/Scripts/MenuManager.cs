using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text highScore;


    public void Start()
    {

        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        
    }
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 1");
    }
    public void DisplayHighScore()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    
    public void Reset()
        {
            PlayerPrefs.DeleteKey("HighScore");
            highScore.text = "0";

        }
    
}

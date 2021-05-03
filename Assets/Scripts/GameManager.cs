using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenuPanel;
    public static GameManager Instance;
    public bool gameOver = false;
    public int score = 0;
    public GameObject gameOverPanel;
    public Text scoreText;
    public Text[] scoreTextPanel;
    public AudioSource audioSource;
    public Text[] highscore;

    private void Start()
    {
        highscore[0].text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        highscore[1].text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void GameOver()
    {
        audioSource.Play();
        scoreText.enabled = false;
        pauseButton.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOver = true;
        scoreTextPanel[0].text =score.ToString();
        scoreTextPanel[1].text =score.ToString();
        GameObject.Find("Point").GetComponent<EnemySpawner>().StopSpawning();
        HighScore();

    }
    public void IncrementScore()
    {
        if (!gameOver)
        {
            score = score + 2;
            scoreText.text = score.ToString();
        }
        
    }
    public void DecrementScore()
    {
        if (!gameOver)
        {
            score--;
           scoreText.text = score.ToString();
        }
    }
    public void IncScore()
    {
        if (!gameOver)
        {
            score++;
            scoreText.text = score.ToString();
        }
        
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
    }
    
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);
    }

    public void HighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highscore[0].text = score.ToString();
            highscore[1].text = score.ToString();
        }
        
    }
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        highscore[0].text = "0";
        highscore[1].text = "0";
    }
}

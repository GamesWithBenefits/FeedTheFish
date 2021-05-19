using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenuPanel;
    public static GameManager instance;
    public bool gameOver;
    public int score;
    public GameObject gameOverPanel;
    public Text scoreText;
    public Text[] scoreTextPanel;
    public AudioSource audioSource;
    public Text[] highscore;
    public int a;
    public Button but;

    private void Start()
    {
        highscore[0].text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        highscore[1].text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        AdsManager.Instance.ShowAds(4);
        AdsManager.Instance.ShowAds(5);

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
        AdsManager.Instance.HideAds(4);
        AdsManager.Instance.HideAds(5);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AdsManager.Instance.HideAds(4);
        AdsManager.Instance.HideAds(5);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
        AdsManager.Instance.HideAds(2);
        AdsManager.Instance.HideAds(3);
    }
    
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);
        AdsManager.Instance.ShowAds(2);
        AdsManager.Instance.ShowAds(3);
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

    public void NotGameOver()
    {
        Time.timeScale = 0f;
        audioSource.Play();
        scoreText.enabled = false;
        pauseButton.SetActive(false);
        gameOverPanel.SetActive(true);
        scoreTextPanel[0].text =score.ToString();
        scoreTextPanel[1].text =score.ToString();
        HighScore();

    }

    public void Continue()
    {
        Time.timeScale = 1f;
        scoreText.enabled = true;
        pauseButton.SetActive(true);
        gameOverPanel.SetActive(false);
    }
    
    public void OnClick()
    {
        but.interactable = false;
        AdsManager.Instance.RewardedVideo();
    }
}

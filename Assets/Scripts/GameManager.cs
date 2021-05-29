using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject rulesPanel;
    public GameObject pauseMenuPanel;
    public static GameManager Instance;
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
        if (PlayerPrefs.GetInt("TutorialPlayed", 0) == 0)
        {
            Time.timeScale = 0f;
            pauseButton.SetActive(false);
            rulesPanel.SetActive(true);
            PlayerPrefs.SetInt("TutorialPlayed", 10);
        }
        else
        {
            rulesPanel.SetActive(false);
        }

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
        AdsManager.Instance.ShowAds(1);
       

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
            if (score < 30)
            {
                score--;
            }
            else if (score >= 30 && score < 120 )
            {
                score = score - 4;
            }
            if (score > 120)
            {
                score = score - 8;
            }
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
        AdsManager.Instance.HideAds(1);
        
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AdsManager.Instance.HideAds(1);
      
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
        rulesPanel.SetActive(false);
        pauseButton.SetActive(true);
        AdsManager.Instance.HideAds(2);
        
    }
    
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);
        pauseButton.SetActive(false);
        AdsManager.Instance.ShowAds(2);
        
    }
    
    public void LoadRules()
    {
        pauseButton.SetActive(false);
        pauseMenuPanel.SetActive(false);
        rulesPanel.SetActive(true);
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

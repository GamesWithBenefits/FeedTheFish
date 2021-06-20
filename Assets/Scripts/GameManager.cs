using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   [SerializeField] private Animator[] lifeAnimation;
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
    public int livesleft = 3;
    public Button but;
    public GameObject[] image;
    private static readonly int PlayAnim = Animator.StringToHash("PlayAnim");


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
        } else
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
    private void GameOver()
    {
        audioSource.Play();
        scoreText.enabled = false;
        pauseButton.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOver = true;
        scoreTextPanel[0].text = score.ToString();
        scoreTextPanel[1].text = score.ToString();
        GameObject.Find("Point").GetComponent<EnemySpawner>().StopSpawning();
        HighScore();
        AdsManager.Instance.ShowAds(3);


    }
    public void IncrementScore()
    {
        if (gameOver)
            return;

        score += 2;
        scoreText.text = score.ToString();

    }
    public void DecrementScore()
    {
        if (gameOver)
            return;

        if (score < 30)
        {
            score--;
        } else if (score >= 30 && score < 120)
        {
            score -= 4;
        }
        if (score > 120)
        {
            score -= 8;
        }
        scoreText.text = score.ToString();
    }
    public void IncScore()
    {
        if (gameOver)
            return;

        score++;
        scoreText.text = score.ToString();

    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        AdsManager.Instance.HideAds(2);
        AdsManager.Instance.HideAds(3);

    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AdsManager.Instance.HideAds(2);

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

    private void HighScore()
    {
        if (score <= PlayerPrefs.GetInt("HighScore", 0))
            return;

        PlayerPrefs.SetInt("HighScore", score);
        highscore[0].text = score.ToString();
        highscore[1].text = score.ToString();

    }
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        highscore[0].text = "0";
        highscore[1].text = "0";
    }

    private void NotGameOver()
    {
        Time.timeScale = 0f;
        audioSource.Play();
        scoreText.enabled = false;
        pauseButton.SetActive(false);
        gameOverPanel.SetActive(true);
        scoreTextPanel[0].text = score.ToString();
        scoreTextPanel[1].text = score.ToString();
        HighScore();

    }

    public async void  Continue()
    {

        await Task.Delay(10);
        Time.timeScale = 0f;
        
        scoreText.enabled = true;
        pauseButton.SetActive(true);
        gameOverPanel.SetActive(false);

        await Task.Delay(5000);
        
        Time.timeScale = 1f;
        
    }

    public void OnClick()
    {
        but.interactable = false;
        AdsManager.Instance.RewardedVideo();
    }

    private void GameLives()
    {
        switch (livesleft)
        {
            case 2:
                lifeAnimation[0].SetBool(PlayAnim, true);
                
                break;
            case 1:
                lifeAnimation[1].SetBool(PlayAnim, true);
                break;
            case 0:
                lifeAnimation[2].SetBool(PlayAnim, true);
                break;
        }
    }

    public void LivesChecker()
    {
        livesleft--;
        GameLives();
        switch (livesleft)
        {
            case -1:
                NotGameOver();
                break;
            case -2:
                GameOver();
                break;
        }
    }
}



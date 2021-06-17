using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject dust;
    private int _randomT;
    public Rigidbody2D rb;
    private Transform _ques;
    void Awake()
    {
        
        _randomT = Random.Range(1, 100);
        _ques = transform.GetChild(0);
        _ques.GetComponent<TextMesh>().text = _randomT.ToString();
    }
    
    void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            Destroy(gameObject);
        }
        ControlSpeed();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameManager.Instance.GameLives();
        if (GameManager.Instance.livesleft >= 0)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                var dustEffect = Instantiate(dust, transform.position, Quaternion.identity);
                Destroy(dustEffect,1f);
                Destroy(gameObject);
                if (_randomT % 2 == 0)
                {
                    GameManager.Instance.livesleft--;
                    if (GameManager.Instance.livesleft == -1)
                    {
                        GameManager.Instance.NotGameOver();
                        GameManager.Instance.livesleft--;

                    }
                    else if (GameManager.Instance.livesleft == -2)
                    {
                        Destroy(col.gameObject);
                        GameManager.Instance.GameOver();
                    }
                }
                else
                {
                    Destroy(gameObject);
                    GameManager.Instance.IncrementScore();
                }
            }
            else if (col.gameObject.CompareTag("Ground"))
            {
                if (_randomT % 2 == 0)
                {
                    GameManager.Instance.IncScore();
                }
                else
                {
                    GameManager.Instance.DecrementScore();
                }

                gameObject.SetActive(false);
                GameObject dustEffect = Instantiate(dust, transform.position, Quaternion.identity);
                Destroy(dustEffect, 2f);
                Destroy(gameObject, 2f);
            }
        }
        
        else if (GameManager.Instance.livesleft == -2)

        {
            GameManager.Instance.NotGameOver();
            GameManager.Instance.livesleft++;
                
        }
        else
        {
            GameManager.Instance.GameOver();
            Destroy(gameObject, 2f);
        }
    }

    void ControlSpeed()
    {
        if (GameManager.Instance.score < 30)
        {
            rb.gravityScale = 0.03f;
        }
        else if (GameManager.Instance.score > 30 && GameManager.Instance.score < 120 )
        {
            rb.gravityScale = 0.1f;
        }
        if (GameManager.Instance.score > 120)
        {
            rb.gravityScale = 0.2f;
        }
    }
}

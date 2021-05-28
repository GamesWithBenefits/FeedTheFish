using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject dust;
    private int _randomT;
    public Rigidbody2D rb;
    private Transform _ques;
    public bool isGamesOver = false;
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
        if (GameManager.Instance.score >= 0)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                GameObject dustEffect = Instantiate(dust, transform.position, Quaternion.identity);
                Destroy(dustEffect,1f);
                Destroy(gameObject);
                if (_randomT % 2 == 0)
                {
                    if (GameManager.Instance.a == 0)
                    {
                        GameManager.Instance.NotGameOver();
                        GameManager.Instance.a++;

                    }
                    else if (GameManager.Instance.a == 1)
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
        
        else if (GameManager.Instance.a == 0)

        {
            GameManager.Instance.NotGameOver();
            GameManager.Instance.a++;
                
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

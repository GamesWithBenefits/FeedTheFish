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
        if (GameManager.instance.gameOver)
        {
            Destroy(gameObject);
        }
        ControlSpeed();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager.instance.score >= 0)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                GameObject dustEffect = Instantiate(dust, transform.position, Quaternion.identity);
                Destroy(dustEffect,1f);
                if (_randomT % 2 == 0)
                {
                    if (GameManager.instance.a == 0)
                    {
                        GameManager.instance.NotGameOver();
                        GameManager.instance.a++;

                    }
                    else if (GameManager.instance.a == 1)
                    {
                        Destroy(col.gameObject);
                        GameManager.instance.GameOver();
                    }
                }
                else
                {
                    Destroy(gameObject);
                    GameManager.instance.IncrementScore();
                }
            }
            else if (col.gameObject.CompareTag("Ground"))
            {
                if (_randomT % 2 == 0)
                {
                    GameManager.instance.IncScore();
                }
                else
                {
                    GameManager.instance.DecrementScore();
                }

                gameObject.SetActive(false);
                GameObject dustEffect = Instantiate(dust, transform.position, Quaternion.identity);
                Destroy(dustEffect, 2f);
                Destroy(gameObject, 2f);
            }
        }
        
        else if (GameManager.instance.a == 0)

        {
            GameManager.instance.NotGameOver();
            GameManager.instance.a++;
                
        }
        else
        {
            GameManager.instance.GameOver();
        }
    }

    void ControlSpeed()
    {
        if (GameManager.instance.score < 30)
        {
            rb.gravityScale = 0.03f;
        }
        else if (GameManager.instance.score > 30 && GameManager.instance.score < 90 )
        {
            rb.gravityScale = 0.1f;
        }
        if (GameManager.instance.score > 90)
        {
            rb.gravityScale = 0.2f;
        }
    }
}

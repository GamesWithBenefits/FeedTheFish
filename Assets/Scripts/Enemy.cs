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
                if (_randomT % 2 == 0)
                {
                    Destroy(col.gameObject);
                    GameManager.Instance.GameOver();
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
        else
        {
            GameManager.Instance.GameOver();
        }
    }

    void ControlSpeed()
    {
        if (GameManager.Instance.score < 30)
        {
            rb.gravityScale = 0.03f;
        }
        else if (GameManager.Instance.score > 30 && GameManager.Instance.score < 90 )
        {
            rb.gravityScale = 0.1f;
        }
        if (GameManager.Instance.score > 90)
        {
            rb.gravityScale = 0.2f;
        }
    }
}

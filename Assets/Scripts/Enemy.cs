using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject dust;
    protected int RandomT;
    public Rigidbody2D rb;
    private Transform _ques;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Awake()
    {
        
        RandomT = Random.Range(1, 100);
        _ques = transform.GetChild(0);
        _ques.GetComponent<TextMesh>().text = RandomT.ToString();
    }

    private void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            Destroy(gameObject);
        }
        ControlSpeed();

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            var dustEffect = Instantiate(dust, transform.position, Quaternion.identity);
            Destroy(dustEffect,1f);
            Destroy(gameObject);
            if (RandomT % 2 == 0)
            {
                GameManager.Instance.LivesChecker();
            }
            else
            {
                GameManager.Instance.IncrementScore();
                Destroy(gameObject);
            }
        }
        else if (col.gameObject.CompareTag("Ground"))
        {
            if (RandomT % 2 == 0)
                GameManager.Instance.IncScore();
            else
            {
                GameManager.Instance.DecrementScore();
                if (GameManager.Instance.score < 0)
                {
                    GameManager.Instance.LivesChecker();
                }
            }

            
            gameObject.SetActive(false);
            var dustEffect = Instantiate(dust, transform.position, Quaternion.identity);
            Destroy(dustEffect, 2f);
            Destroy(gameObject, 2f);
        }
    }

    private void ControlSpeed()
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

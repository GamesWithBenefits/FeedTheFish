using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _xInput;
    public float moveSpeed;
    private Vector2 _newPosition;
    public float xPositionLimit;
    public AudioSource audioSource;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        _xInput = Input.acceleration.x;
        _newPosition = transform.position;
        _newPosition.x  = _newPosition.x + _xInput*moveSpeed;
        _newPosition.x = Mathf.Clamp(_newPosition.x, -xPositionLimit, xPositionLimit);
        _rb.MovePosition(_newPosition);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            audioSource.Play();
        }
        else if (GameManager.instance.gameOver)
        {
            audioSource.Stop();
        }
    }
}

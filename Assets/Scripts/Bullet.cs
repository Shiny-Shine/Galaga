using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bSpeed = 500f;
    private Rigidbody2D rb2d;
    private Vector3 moveUp;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveUp = new Vector2(0f, 1f);
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(transform.position + (moveUp * bSpeed * Time.deltaTime));
        if (transform.position.y > Camera.main.orthographicSize)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("충돌2");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameManager.instance.Life -= 1;
            if (GameManager.instance.Life == 0)
                GameManager.instance.gameOver();
            GameManager.instance.txtUpdate();
        }
    }
}
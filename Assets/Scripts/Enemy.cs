using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator ani;
    public int hp = 0;

    void Start()
    {
        if (gameObject.name == "Enemy3(Clone)")
        {
            hp = 2;
            ani = gameObject.GetComponent<Animator>();
        }
        else hp = 1;
    }

    private void Update()
    {
        
    }

    // 적이 쏜 총알 or 몸통박치기
    private void OnTriggerEnter2D(Collider2D col)
    {
        hp--;
        if (hp == 1)
        {
            ani.SetTrigger("Hit");
        }

        // 몸통박치기
        if (col.gameObject.tag == "Player")
        {
            GameManager.instance.Life -= 1;
            if (GameManager.instance.Life == 0)
                GameManager.instance.gameOver();
            GameManager.instance.txtUpdate();
            if (hp <= 0) Destroy(gameObject);
            return;
        }
        
        GameManager.instance.Score += 50;
        GameManager.instance.txtUpdate();
        if (hp <= 0) Destroy(gameObject);
    }
}
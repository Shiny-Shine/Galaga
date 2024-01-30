using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public BezierCurve bezScr;
    public Animator ani;
    public GameObject bulletPref;
    public int hp = 0;
    public float waitTime;

    void Start()
    {
        bezScr = GetComponent<BezierCurve>();
        ani = gameObject.GetComponent<Animator>();
        if (gameObject.name == "Enemy3(Clone)")
            hp = 2;
        else hp = 1;

        waitTime = Random.Range(7f, 60f);
        StartCoroutine(attack());
    }

    IEnumerator attack()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(waitTime);
            bezScr.enemyAttack();
            Instantiate(bulletPref, transform.position, quaternion.identity);
        }
    }

    void enemyDeath()
    {
        Destroy(gameObject);
    }

    void unitDeath()
    {
        Destroy(gameObject);
    }

    // 적이 쏜 총알 or 몸통박치기
    private void OnTriggerEnter2D(Collider2D col)
    {
        hp--;
        if (hp == 1)
        {
            ani.SetTrigger("Hit");
        }

        GameManager.instance.Score += 50;
        GameManager.instance.txtUpdate();
        if (hp <= 0)
        {
            ani.SetTrigger("Death");
            bezScr.speed = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
    }
}
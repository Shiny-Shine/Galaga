using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 300f;

    public float MaxShootDelay;
    public float NowShootDelay;
    public bool canFire;

    public GameObject bulletObj;

    private void Start()
    {
        canFire = true;
        MaxShootDelay = 1f;
        NowShootDelay = 0f;
        Speed = 300f;
    }

    void Update()
    {
        Move();
        Fire();
    }

    void Move()
    {
        // 좌우 버튼을 눌렀을 때
        float h = Input.GetAxisRaw("Horizontal");
        //float w = Input.GetAxisRaw("Vertical");

        // 위치 이동
        Vector2 nowPos = transform.position;
        Vector2 afterPos = new Vector2(h, 0) * Speed * Time.deltaTime;
        if (Math.Abs(nowPos.x + afterPos.x) >= 320)
            afterPos.x = 0;
        //if (Math.Abs(nowPos.y + afterPos.y) >= 590)
        //    afterPos.y = 0;
        transform.position = nowPos + afterPos;
    }

    void Fire()
    {
        if (Input.GetKeyDown("space") && canFire)
        {
            canFire = false;
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 75, 0f);
            Instantiate(bulletObj, pos, Quaternion.identity);
        }

        if (!canFire)
        {
            NowShootDelay += Time.deltaTime;
            if (NowShootDelay >= MaxShootDelay)
            {
                canFire = true;
                NowShootDelay = 0f;
            }
        }
    }
}
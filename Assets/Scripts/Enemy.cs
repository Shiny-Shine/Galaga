using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameManager.instance.Life -= 1;
            GameManager.instance.txtUpdate();
            return;
        }

        GameManager.instance.Score += 50;
        GameManager.instance.txtUpdate();
        Destroy(gameObject);
    }
}
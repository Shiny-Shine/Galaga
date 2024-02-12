using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float speed;
    
    void Start()
    {
        speed = 200f;
    }
    
    void FixedUpdate()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
        if (transform.position.y <= -Camera.main.orthographicSize * 2)
        {
            transform.position = new Vector3(curPos.x, Camera.main.orthographicSize * 2, 0);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 200f;
    }

    // Update is called once per frame
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

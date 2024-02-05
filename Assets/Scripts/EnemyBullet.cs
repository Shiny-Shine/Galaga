using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bSpeed = 600f;
    private Rigidbody2D rb2d;
    private Vector2 newPos;
    private Vector2 direction;
    private Vector3 targetPosition;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // 소환시 플레이어를 향하도록함
        if (GameManager.instance.playerObj != null)
            newPos = GameManager.instance.playerObj.transform.position - transform.position;
        else
            Destroy(gameObject);
        float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ - 90f);

        float z = transform.rotation.eulerAngles.z + 90f;
        direction = new Vector2(Mathf.Cos(z * Mathf.Deg2Rad), Mathf.Sin(z * Mathf.Deg2Rad));
        targetPosition = new Vector3(direction.x, direction.y, 0f);
    }

    private void FixedUpdate()
    {
        // 화면 아래로 bullet 나가면 파괴 
        if (transform.position.y < Camera.main.transform.position.y -
            Camera.main.orthographicSize)
            Destroy(gameObject);

        rb2d.MovePosition(transform.position + (targetPosition * bSpeed * Time.deltaTime));
    }
}
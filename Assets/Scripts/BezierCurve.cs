using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class BezierCurve : MonoBehaviour
{
    public List<Vector3> points;
    public Transform[] waypoints = new Transform[4];
    public bool isCurving;
    private float zIdx = 0f;
    public float speed = 0f;

    private Vector3 bezierPos;
    private Vector3 prePos;
    private Vector2 gizmoPos;

    public bool Curving
    {
        get { return isCurving; }
        set { isCurving = value; }
    }

    void Start()
    {
        
    }

    /*
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCurving)
        {
            gameObject.transform.position = Bezier(ref points, t);
            t += tIncrease;
            
            if (t >= 1f)
            {
                isCurving = false;
                gameObject.transform.rotation = quaternion.identity;
            }
        }
    }
    */

    // 유니티 씬 뷰에서 미리 궤적을 확인할 수 있는 Gizmo
    private void OnDrawGizmos()
    {
        if (waypoints[0] == null)
            return;

        for (float t = 0; t < 1; t += 0.05f)
        {
            // 조절점이 4개일 때 베지어 곡선 공식
            // P = (1−t)³P₁ + 3(1−t)²tP₂ +3(1−t)t²P₃ + t³P₄
            gizmoPos = Mathf.Pow(1 - t, 3) * waypoints[0].position
                       + 3 * Mathf.Pow(1 - t, 2) * t * waypoints[1].position
                       + 3 * (1 - t) * Mathf.Pow(t, 2) * waypoints[2].position
                       + Mathf.Pow(t, 3) * waypoints[3].position;
            
            Gizmos.DrawSphere(gizmoPos, 30f);
            Gizmos.DrawLine(new Vector2(waypoints[0].position.x, waypoints[0].position.y),
                new Vector2(waypoints[1].position.x, waypoints[1].position.y));
            Gizmos.DrawLine(new Vector2(waypoints[2].position.x, waypoints[2].position.y),
                new Vector2(waypoints[3].position.x, waypoints[3].position.y));
        }
    }
    
    IEnumerator BezireLining()
    {
        // 4점 베지어 곡선 2개
        for (int i = 0; i < 2; i++)
        {
            for (float t = 0; t < 1; t += Time.deltaTime * speed)
            {
                // 조절점이 4개일 때 베지어 곡선 공식
                // P = (1−t)³P₁ + 3(1−t)²tP₂ +3(1−t)t²P₃ + t³P₄
                bezierPos = Mathf.Pow(1 - t, 3) * waypoints[0].position
                            + 3 * Mathf.Pow(1 - t, 2) * t * waypoints[1].position
                            + 3 * (1 - t) * Mathf.Pow(t, 2) * waypoints[2].position
                            + Mathf.Pow(t, 3) * waypoints[3].position;

                transform.position = bezierPos;

                var angle = RotateDir(prePos, transform.position);
                prePos = transform.position;
                
                transform.rotation = Quaternion.Euler(0, 0, 180 - angle);

                yield return new WaitForEndOfFrame();
            }
        }
    }

    
    private float RotateDir(Vector3 prePos, Vector3 nowPos)
    {
        Vector3 dir = nowPos - prePos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle;
    }

    /*
    Vector3 Bezier(ref List<Vector3> points, float t)
    {
        if (points.Count <= 1)
        {
            RotateDir(points[0]);
            return points[0];
        }

        List<Vector3> newPoints = new List<Vector3>();
        for (int i = 0; i < points.Count - 1; i++)
        {
            // 각 선분 시작점에서 t에 비례하는 위치에 점을 찍는다.
            newPoints.Add(BezierPos(points[i], points[i + 1], t));
        }

        return Bezier(ref newPoints, t);
    }

    // 두 위치 사이의 선분에서 t에 비례하는 위치를 리턴
    // P = (1-t)P1 + tP2
    Vector3 BezierPos(Vector3 p1, Vector3 p2, float t)
    {
        float time = 1 - t;
        float x = (time * p1.x + t * p2.x);
        float y = (time * p1.y + t * p2.y);

        return new Vector3(x, y, zIdx);
    }
    */
}
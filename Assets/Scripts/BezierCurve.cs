using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform[] waypoints = new Transform[8];
    public Transform arrivePos;
    public bool isCurving;
    private float zIdx = 0f;
    public float speed = 0.05f;

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
        for (int i = 0; i < 8; i++)
        {
            waypoints[i] = PointManager.pInstance.wayPoints[i];
        }
        StartCoroutine(BezireLining());
    }

    // 유니티 씬 뷰에서 미리 궤적을 확인할 수 있는 Gizmo
    private void OnDrawGizmos()
    {
        if (waypoints[0] == null)
            return;

        for (int i = 0; i < 2; i++)
        {
            for (float t = 0; t < 1; t += 0.05f)
            {
                // 조절점이 4개일 때 베지어 곡선 공식
                // P = (1−t)³P₁ + 3(1−t)²tP₂ +3(1−t)t²P₃ + t³P₄
                gizmoPos = Mathf.Pow(1 - t, 3) * waypoints[i * 4 + 0].position
                           + 3 * Mathf.Pow(1 - t, 2) * t * waypoints[i * 4 + 1].position
                           + 3 * (1 - t) * Mathf.Pow(t, 2) * waypoints[i * 4 + 2].position
                           + Mathf.Pow(t, 3) * waypoints[i * 4 + 3].position;

                Gizmos.DrawSphere(gizmoPos, 30f);
                Gizmos.DrawLine(new Vector2(waypoints[i * 4 + 0].position.x, waypoints[i * 4 + 0].position.y),
                    new Vector2(waypoints[i * 4 + 1].position.x, waypoints[i * 4 + 1].position.y));
                Gizmos.DrawLine(new Vector2(waypoints[i * 4 + 2].position.x, waypoints[i * 4 + 2].position.y),
                    new Vector2(waypoints[i * 4 + 3].position.x, waypoints[i * 4 + 3].position.y));
            }
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
                bezierPos = Mathf.Pow(1 - t, 3) * waypoints[i * 4 + 0].position
                            + 3 * Mathf.Pow(1 - t, 2) * t * waypoints[i * 4 + 1].position
                            + 3 * (1 - t) * Mathf.Pow(t, 2) * waypoints[i * 4 + 2].position
                            + Mathf.Pow(t, 3) * waypoints[i * 4 + 3].position;

                RotateDir(bezierPos);
                bezierPos.z = 0;
                transform.position = bezierPos;

                yield return null;
            }
        }

        RotateDir(arrivePos.position);
        for (float t = 0; t < 1; t += Time.deltaTime * speed)
        {
            transform.position = Vector3.MoveTowards(transform.position, arrivePos.position, t);
            yield return null;
        }
            

        // 이동이 끝나고 수직으로 회전해 정지
        bool isRot = true;
        while (isRot)
        {
            var rot = transform.rotation;
            // Lerp = 두개의 회전값과 비율에 따라 적당한 회전값을 리턴
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion.identity, Time.deltaTime * 10f);
            if (rot == transform.rotation)
            {
                isRot = false;
                transform.rotation = Quaternion.identity;
            }

            yield return null;
        }
    }


    private void RotateDir(Vector3 nextPos)
    {
        Vector3 dir = nextPos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
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
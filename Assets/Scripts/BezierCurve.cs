using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class BezierCurve : MonoBehaviour
{
    public Transform[] insWaypoints = new Transform[8];
    public float speed = 0.05f;

    private Vector3 bezierPos;
    private Vector3 prePos;
    private Vector2 gizmoPos;

    void Start()
    {
        StartCoroutine(BezireLining());
    }

    // 유니티 씬 뷰에서 미리 궤적을 확인할 수 있는 Gizmo
    private void OnDrawGizmos()
    {
        if (insWaypoints[0] == null)
            return;

        for (int i = 0; i < 2; i++)
        {
            for (float t = 0; t < 1; t += 0.05f)
            {
                // 조절점이 4개일 때 베지어 곡선 공식
                // P = (1−t)³P₁ + 3(1−t)²tP₂ +3(1−t)t²P₃ + t³P₄
                gizmoPos = Mathf.Pow(1 - t, 3) * insWaypoints[i * 4 + 0].position
                           + 3 * Mathf.Pow(1 - t, 2) * t * insWaypoints[i * 4 + 1].position
                           + 3 * (1 - t) * Mathf.Pow(t, 2) * insWaypoints[i * 4 + 2].position
                           + Mathf.Pow(t, 3) * insWaypoints[i * 4 + 3].position;

                Gizmos.DrawSphere(gizmoPos, 30f);
                Gizmos.DrawLine(new Vector2(insWaypoints[i * 4 + 0].position.x, insWaypoints[i * 4 + 0].position.y),
                    new Vector2(insWaypoints[i * 4 + 1].position.x, insWaypoints[i * 4 + 1].position.y));
                Gizmos.DrawLine(new Vector2(insWaypoints[i * 4 + 2].position.x, insWaypoints[i * 4 + 2].position.y),
                    new Vector2(insWaypoints[i * 4 + 3].position.x, insWaypoints[i * 4 + 3].position.y));
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
                bezierPos = Mathf.Pow(1 - t, 3) * insWaypoints[i * 4 + 0].position
                            + 3 * Mathf.Pow(1 - t, 2) * t * insWaypoints[i * 4 + 1].position
                            + 3 * (1 - t) * Mathf.Pow(t, 2) * insWaypoints[i * 4 + 2].position
                            + Mathf.Pow(t, 3) * insWaypoints[i * 4 + 3].position;

                RotateDir(bezierPos);
                bezierPos.z = 0;
                transform.position = bezierPos;

                yield return null;
            }
        }

        RotateDir(insWaypoints[7].position);

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
}
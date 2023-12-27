using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public float t = 0f;
    public List<Vector3> points;
    public List<Transform> Transforms;
    void Start()
    {
        for (int i = 0; i < Transforms.Count; i++)
        {
            points.Add(Transforms[i].position);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (t >= 1f)
        {
            points.Clear();
            t = 0f;
            for (int i = 0; i < Transforms.Count; i++)
            {
                points.Add(Transforms[i].position);
            }
        }
        gameObject.transform.position = Bezier(ref points, t);
        t += 0.01f;
    }
    
    Vector3 Bezier(ref List<Vector3> points, float t)
    {
        if (points.Count <= 1)
        {
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

        return new Vector3(x, y, 0);
    }
}

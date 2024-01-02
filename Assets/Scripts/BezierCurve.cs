using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class BezierCurve : MonoBehaviour
{
    public GameObject pointObj;
    public float t = 0f;
    public List<Vector3> points;
    public List<GameObject> objs;
    public bool isCurving;
    public TrailRenderer trail;
    void Start()
    {
        trail.enabled = false;
        isCurving = false;
        for (int i = 0; i < objs.Count; i++)
        {
            points.Add(objs[i].transform.position);
        }
    }

    public void btnStart()
    {
        isCurving = true;
        t = 0f;
        points.Clear();
        gameObject.transform.position = objs[0].transform.position;
        trail.enabled = true;
        for (int i = 0; i < objs.Count; i++)
        {
            points.Add(objs[i].transform.position);
        }
    }
    
    public void btnDelete()
    {
        objs[objs.Count - 1].GetComponent<BezierPoint>().btnDelete();
        objs.RemoveAt(objs.Count - 1);
        points.RemoveAt(points.Count - 1);
    }

    public void btnAdd()
    {
        GameObject newObj = Instantiate(pointObj, new Vector3(0f, 0f, 0f), quaternion.identity);
        newObj.GetComponentInChildren<TextMeshPro>().text = $"{objs.Count + 1}";
        objs.Add(newObj);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCurving)
        {
            if (t >= 1f)
            {
                isCurving = false;
            }
            gameObject.transform.position = Bezier(ref points, t);
            t += 0.01f;
        }
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

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            //생성 전이면
            instance = this; //생성
        }
        else if (instance != this)
        {
            //이미 생성되어 있으면
            Destroy(this.gameObject); //새로만든거 삭제
        }

        DontDestroyOnLoad(this.gameObject); //씬이 넘어가도 오브젝트 유지
    }

    /*
    public void btnStart()
    {
        if(dotObj != null)
            Destroy(dotObj);
        dotObj = Instantiate(dotPref, points[0].transform.position, Quaternion.identity);
        dotScr = dotObj.GetComponent<BezierCurve>();
        dotScr.Curving = true;
    }

    public void btnDelete()
    {
        if (points.Count <= 2)
            return;
        points[points.Count - 1].GetComponent<BezierPoint>().btnDelete();
        points.RemoveAt(points.Count - 1);
    }

    public void btnAdd()
    {
        pointObj = Instantiate(pointPref, new Vector3(0f, 0f, 0f), Quaternion.identity);
        pointObj.GetComponentInChildren<TextMeshPro>().text = $"{points.Count + 1}";
        points.Add(pointObj);
    }
    */
}
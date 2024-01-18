using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager pInstance;
    public Transform[] arrivePoints, wayPoints;
    
    private void Awake()
    {
        if (pInstance == null)
        {
            pInstance = this;
        }
        else if (pInstance != this)
        {
            //이미 생성되어 있으면
            Destroy(this.gameObject); //새로만든거 삭제
        }
    }
}

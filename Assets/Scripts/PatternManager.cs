using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public static PatternManager pInstance;
    public Transform[] arrivePoints, wayPoints;
    public Texture[] textures;
    public GameObject[] enemyPref;
    private int count = 0;
    private GameObject enemyObj;
    private BezierCurve enemyScr;

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

    public void waveStart()
    {
        StartCoroutine("spawn");
    }

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(3f);
        count = 0;
        // 적 기체 생성 및 이동 궤적 지정
        yield return wave(0, 0, 1, 1, 0.05f);
        yield return wave(2, 2, 1);
        yield return wave(3, 1);
        yield return wave(0, 0);
        yield return wave(1, 0);
    }

    IEnumerator wave(int line, int color, int color2 = -1, int line2 = -1, float spawnTime = 0.15f)
    {
        for (int i = 0; i < 8; i++)
        {
            if (color2 >= 0 && i % 2 != 0) enemyObj = Instantiate(enemyPref[color2], wayPoints[0].position, Quaternion.identity);
            else enemyObj = Instantiate(enemyPref[color], wayPoints[0].position, Quaternion.identity);

            if (line2 >= 0 && i % 2 != 0) enemySet(line2);
            else enemySet(line);
            yield return new WaitForSeconds(spawnTime);
        }
        yield return new WaitForSeconds(3.5f);
    }

    private void enemySet(int line)
    {
        enemyScr = enemyObj.GetComponent<BezierCurve>();
        enemyScr.speed += (GameManager.instance.Stage - 1) * 0.1f;
        enemyScr.idx = count;
        enemyScr.insWaypoints[7] = arrivePoints[count++];
        for (int j = 0; j < 7; j++)
        {
            enemyScr.insWaypoints[j] = wayPoints[(line * 8) + j];
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < arrivePoints.Length; i++)
        {
            if (i % 10 == 0)
                Gizmos.color = new Color(0.2f, 0.8f, 1f);
            else if (i % 10 < 5)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(arrivePoints[i].position, 15f);
            if (textures[0] != null)
            {
                Gizmos.DrawGUITexture(
                    new Rect(arrivePoints[i].position.x - 27f, arrivePoints[i].position.y - 13.5f, 20, -20),
                    textures[i / 10]);
                Gizmos.DrawGUITexture(
                    new Rect(arrivePoints[i].position.x + 0f, arrivePoints[i].position.y - 13.5f, 20, -20),
                    textures[i % 10]);
            }
        }
    }
}
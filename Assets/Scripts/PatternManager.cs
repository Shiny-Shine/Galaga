using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public Texture[] textures;
    public Transform[] arrivePoints, wayPoints;
    public GameObject[] enemyPref;
    private GameObject enemyObj;
    private BezierCurve enemyScr;

    private void Awake()
    {
        /*
        if (pInstance == null)
        {
            pInstance = this;
        }
        else if (pInstance != this)
        {
            //이미 생성되어 있으면
            Destroy(this.gameObject); //새로만든거 삭제
        }
        */
    }

    private void Start()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(0.15f);
        // 적 기체 생성 및 이동 궤적 지정
        for (int t = 0; t < 4; t++)
        {
            int line = Random.Range(0, wayPoints.Length / 8);
            Debug.Log(line);
            for (int i = 0; i < 10; i++)
            {
                enemyObj = Instantiate(enemyPref[t], wayPoints[line].position, Quaternion.identity);
                enemyScr = enemyObj.GetComponent<BezierCurve>();
                enemyScr.insWaypoints[7] = arrivePoints[(t * 10) + i];
                for (int j = 0; j < 7; j++)
                {
                    enemyScr.insWaypoints[j] = wayPoints[(line * 8) +  j];
                }

                yield return new WaitForSeconds(0.15f);
            }
            yield return new WaitForSeconds(5f);
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
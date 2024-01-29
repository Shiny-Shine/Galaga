using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private int score = 0, life = 3, stage = 0;
    private TMP_Text scoreTxt, lifeTxt, hScoreTxt, stageTxt;
    public GameObject stageBObj, gameverObj;

    public int Life
    {
        get { return life; }
        set { life = value; }
    }
    
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    
    public int Stage
    {
        get { return stage; }
        set { stage = value; }
    }

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

    private void Start()
    {
        stage = 0;
        life = 3;
        score = 0;
        scoreTxt = GameObject.Find("Score").GetComponent<TMP_Text>();
        lifeTxt = GameObject.Find("Life").GetComponent<TMP_Text>();
        hScoreTxt = GameObject.Find("hScore").GetComponent<TMP_Text>();
        stageTxt = GameObject.Find("Stage").GetComponent<TMP_Text>();
        stageBObj = GameObject.Find("StageBanner");
        gameverObj = GameObject.Find("GameOver");
        stageStart();
    }

    void stageStart()
    {
        Stage++;
        stageBObj.SetActive(true);
        PatternManager.pInstance.waveStart();
    }

    public void txtUpdate()
    {
        scoreTxt.text = String.Format("{0:D6}", score);
        lifeTxt.text = String.Format("Life = {0}", life);
    }

    public void stageUpdate()
    {
        stage++;
        stageTxt.text = String.Format("Stage {0}", stage);
        stageBObj.SetActive(true);
    }

    public void gameOver()
    {
        gameverObj.SetActive(true);
        hScoreTxt.text = String.Format("{0:D6}", score);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public TMP_Text scoreTxt, lifeTxt;
    public int Score = 0, Life = 3;

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
        scoreTxt = GameObject.Find("Score").GetComponent<TMP_Text>();
        lifeTxt = GameObject.Find("Life").GetComponent<TMP_Text>();
    }

    public void txtUpdate()
    {
        scoreTxt.text = String.Format("{0:D6}", Score);
        lifeTxt.text = String.Format("Life = {0}", Life);
    }
}
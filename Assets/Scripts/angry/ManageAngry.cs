using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using Management;
using TMPro;
using Random = UnityEngine.Random;

public class ManageAngry : Manage
{
    public static ManageAngry instAM;
    public Transform _parent;   // 에디터에서 참조 연결
    public CameraMove mainCamScr;
    public int gameMode=0;
    public int enemiesDead = 0;

    public GameObject _plank, _bird;   // 프리펩
    private float _wid = 1.6f;  // plank 가로

    [SerializeField] private TMP_Text _txtScore, _txtBest, _txtLife;
    [SerializeField] private int _score = 0, _life = 3;
    
    public int Score
    {
        set { _score = value; }
        get { return _score;  }
    }

    public int Life
    {
        set { _life = value; }
        get { return _life;  }
    }
    
    private void Awake()
    {
        instAM = this;
        base.Awake();
        _plank = (GameObject)Resources.Load("Plank");
        _bird = (GameObject)Resources.Load("Bird");

        _txtScore = GameObject.Find("txtScore").GetComponent<TMP_Text>();
        _txtBest = GameObject.Find("txtBest").GetComponent<TMP_Text>();
        _txtLife = GameObject.Find("txtLife").GetComponent<TMP_Text>();
        //_txtBest.text = string.Format("Bestscore : {0}", ManageApp.Inst.BestA);
    }

    void Start()
    {
        Debug.Log("start");
        Spawn();
    }
    
    public void UpdateCurScore()
    {
        _txtScore.text = String.Format("Score : {0}", instAM._score);
        _txtLife.text = String.Format("Life : {0}", instAM._life);
    }

    public void Spawn()
    {
        int maxcol = 8; // 1층은 최대 8칸
        for (int r = 0; r <= 2; r++)
        {
            maxcol = Random.Range(1, 1 + maxcol);   // 상위층은 아래층 최대칸 수를 참고
            Debug.Log(maxcol);
            CreateRows(r, maxcol);
        }
        mainCamScr.toInit = true;
    }

    void CreateRows(int row, int col)
    {
        float s = _wid * (-col / 2) - (_wid / 2) * (col % 2);
        for (int i = 0; i < col + 1; i++) CreatePlank(s, row, i, true);
        for (int i = 0; i < col; i++) CreatePlank(s + _wid / 2, row, i, false);

        GameObject o = Instantiate(_bird, transform.position, Quaternion.identity);
        o.transform.SetParent(_parent);
        float x = s + _wid / 2 + Random.Range(0, col) * _wid;
        float y = -0.5f + 2f * row;
        o.transform.localPosition = new Vector2(x, y);
    }


    void CreatePlank(float s, int r, int c, bool v)
    {
        GameObject o = Instantiate(_plank, transform.position, Quaternion.identity);
        o.transform.SetParent(_parent);
        if(v)   // 세로로 세워지는 판자
        {
            o.transform.localRotation = Quaternion.Euler(0, 0, 90);
            o.transform.localPosition = new Vector2(s + c * _wid, r * 2);
        }
        else
        {
            // 가로로 누워지는 판자
            o.transform.localPosition = new Vector2(s + c * _wid, r * 2 + 1);
        }
    }

    void setCountDown()
    {
        InstantiateUI("txtCount", "Canvas", false);
    }
    
    public void SetGameOver()
    {
        instAM.gameMode = 2;
    }

    public override void SetStart()
    {

    }
}

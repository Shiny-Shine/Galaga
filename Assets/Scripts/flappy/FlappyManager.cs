using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlappyManager : MonoBehaviour {
    public static FlappyManager instFM;
    public bool isGameOver = false;
    public int gameMode=0;
    private GameObject pGameOver;
    private TMP_Text tScore, tBest, tLife;
    private int score = 0, life = 3;

    public int Life {
        get { return life; }
    }

    public int Score {
        get { return score; }
    }

    void Awake () {
        instFM = this;
        tScore=GameObject.Find("txtScore").GetComponent<TMP_Text>();
        tBest=GameObject.Find("txtBest").GetComponent<TMP_Text>();
        tLife=GameObject.Find("txtLife").GetComponent<TMP_Text>();
        pGameOver = GameObject.Find ("boardResult");
        pGameOver.SetActive (false);
        tBest.text=$"{MngApp.inst.Name}\nBest Score : {MngApp.inst.Best}";
    }

    public void SetGameOver () {
        FlappyManager.instFM.gameMode=2;
        isGameOver = true;
        pGameOver.SetActive (true);
    }

    public void SetAddScore () {
        tScore.text = string.Format ("Score : {0}", score += 10);
    }
    
    public void SetLifeDown() {
        tLife.text = string.Format("Life : {0}", --life);
    }

    public void NextScene(int index) {// 결과창에서 버튼 클릭 함수로 사용됨.
        SceneManager.LoadScene(index);
    }
}
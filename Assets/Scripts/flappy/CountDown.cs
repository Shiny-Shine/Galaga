using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour {
    private TMP_Text textCount;
    private int count;

    void Start() {
        count = 3;
        textCount = GetComponent<TMP_Text>();
    }

    public void ChangeCount() {
        count--;// 카운트 다운한다.
        textCount.text = (count >= 0) ? "" + count : "GO";// 마지막으로 시작을 알린다.

        if (count < -1) {// 'GO'표시마저 사라졌고 이제 게임을 시작시킨다.
            FlappyManager.instFM.gameMode = 1;// Game play mode
            GameObject.Find("Game Manager").GetComponent<ObstaclePool>()
                .InitColumnCreate();// 장애물 생성시킨다.
            GameObject.Find("Bird").SendMessage("GameStart");// 버드의 Rigidbody 활성화
            GameObject[] objs = GameObject.FindGameObjectsWithTag("HorzScroll");
            foreach (var o in objs) // tag가 "HorzScroll"로 지정된 모든 오브젝트를 찾는다.
                o.SendMessage("GameStart"); // 횡 스크롤이 시작되도록 한다.
            Destroy(gameObject);// 카운트 다운 Text 오브젝트를 메모리에서 제거한다.
        }
    }
}
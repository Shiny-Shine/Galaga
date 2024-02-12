using HorzTools;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(Rigidbody2D))]
public class HorzScroll : HScroll {
    private int buildIndex;
    void Awake() {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Start () {
        GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Kinematic; 
        GameStart();
    }

    void Update () {
        if (FlappyManager.instFM != null && FlappyManager.instFM.isGameOver)
            setStop ();
    }

    void GameStart() {
        setRigidbody(2);
    }
}
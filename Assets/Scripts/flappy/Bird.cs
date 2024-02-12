using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Bird : MonoBehaviour {
    public float upForce = 200f; // 에디터에 설정 가능.
    private Rigidbody2D _rb2d;
    private Animator anim;

    private PolygonCollider2D coll;// isTrigger 값 조정.
    private SpriteRenderer renderer;// 플레이어(새)의 껌벅껌벅 효과 처리.

    private bool isBlink = false, isShow = true;
    private int blinkCount = 5;
    private float fBlink = 3f;

    void Start () {
        // RequireComponent를 통해 반드시 컴포넌트가 구성될 예정이므로, 객체의 주소값을 받아둔다. 
        _rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        _rb2d.bodyType=RigidbodyType2D.Kinematic;

        renderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<PolygonCollider2D>();
        coll.isTrigger = true;// 처음에는 충돌시 물리적 처리를 하지 않도록 한다.
    }

    void Update () {
        if (FlappyManager.instFM.gameMode!=1) return;
        
        if (Input.GetMouseButtonDown (0)) {
            // Rigidbody에 적정 힘을 가해서 계산을 하도록 처리한다. 
            _rb2d.velocity = Vector2.zero;
            _rb2d.AddForce (new Vector2 (0f, upForce));
            anim.SetTrigger ("SetFlap"); // aniFlap 재생
        }
        
        BirdBlink(); // 매 프레임마다 껌벅껌벅 렌더링 상태를 확인 및 처리한다
    }

    void OnTriggerEnter2D(Collider2D col) {// 콜라이더가 겹쳐지는 상황에서 호출
        if (col.gameObject.name.Contains("Ground")) {
            coll.isTrigger=false;
            anim.SetTrigger ("SetDie"); // aniDie 재생
            FlappyManager.instFM.SetGameOver ();
            return;
        }

        if (isBlink) return;// 껌벅거리고 있는 상태이면 그냥 종료
        if (col.gameObject.tag != "Column") return;// host 오브젝트 tag 'Column'인지 확인

        isBlink = true;// 껌벅거리기 시작할 것임.
        fBlink = 3f;// 3초 동안 껌벅거릴 것임.
        FlappyManager.instFM.SetLifeDown();
    }

    void OnCollisionEnter2D (Collision2D other) {
        anim.SetTrigger ("SetDie"); // aniDie 재생
        FlappyManager.instFM.SetGameOver ();
    }

    void GameStart() {
        _rb2d.bodyType=RigidbodyType2D.Dynamic;
    }

    void BirdBlink() {
        if (!isBlink) return; // 껌벅거리는 상황이 아니면 종료.

        if (--blinkCount <= 0) {// blinkCount 횟수동안 투명 or 불투명을 유지시킨다.
            blinkCount = 5;
            if (isShow = !isShow) renderer.color = Color.white;// 불투명. 보인다.
            else renderer.color = Color.clear;// 투명. 안보인다.
        }

        fBlink -= Time.deltaTime;
        if (fBlink < 0f) {// 껌벅거리는 시간 종료.
            isBlink = false;// 껌벅거리는 상태 제거.
            renderer.color = Color.white;// 원래대로 불투명으로 복귀.
            if (FlappyManager.instFM.Life==0) coll.isTrigger=false;
        }
    }
}
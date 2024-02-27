using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Bomb : MonoBehaviour
{
    private bool clickedOnBomb = false;

    private Ray _rayToCatapult; // 최대 제한 거리 지정을 위한 레이저빔
    private float _maxLengh = 3f; // 최대 제한 거리
    public Transform _zeroPoint; // 원점. 유니티 에디터에서 지정함.

    private Rigidbody2D _rb2d;
    private SpringJoint2D _spring;
    private bool _springDestroy = false;
    private bool ColliderTrigger = false; // OnCollisionEnter 한번만 호출하도록

    private Vector2 _prev_velocity; // 매 프레임 rigidbody의 바로 전 속도.

    public GameObject nextBomb; // 다음으로 날릴 Bomb

    // Bomb Prefab 
    public GameObject preBomb;

    // Catapult Line
    private LineRenderer _lineback, _linefore; // stone 뒤,앞 라인 
    private bool _isShowLine = true; // line 보이기 여부 

    public bool SpringDestroy
    {
        get { return _springDestroy; }
    }


    void Start()
    {
        preBomb = Resources.Load("Bomb") as GameObject;
        _zeroPoint = GameObject.Find("Catapult Position").GetComponent<Transform>();
        // _zeroPoint에서 Vector3.zero(0,0,0)으로의 Ray 생성
        _rayToCatapult = new Ray(_zeroPoint.position, Vector3.zero);

        _rb2d = GetComponent<Rigidbody2D>();
        _spring = GetComponent<SpringJoint2D>();
        _springDestroy = false;


        // catapult line
        _lineback = GameObject.Find("LineBack").GetComponent<LineRenderer>();
        _linefore = GameObject.Find("LineFore").GetComponent<LineRenderer>();
        createLine();
    }

    private void Update()
    {
        if (clickedOnBomb)
        {
            // 스크린 스페이스의 클릭 지점으로 월드 스페이스의 공산에서의 위치 이동을 시켜야 하는 상황.
            // 스크린 스페이스의 클릭 지점이 월드 스페이스에서는 어느 위치린지 전환처리를 해야 한다.
            Vector3 mouseWorldPoint =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // mouseWorldPoint에서 zeroPoint를 뺌으로서 벡터의 길이가됨.
            Vector2 _newVector = mouseWorldPoint - _zeroPoint.position; // 새 벡터 지정.
            // sqrMagnitude는 벡터의 제곱값 구함, 따라서 비교할때도 _maxLength * _maxLength와 비교
            if (_newVector.sqrMagnitude > _maxLengh * _maxLengh) // 제한거리보다 멀리 있다면
            {
                _rayToCatapult.direction = _newVector; // ray 지정하고
                mouseWorldPoint = _rayToCatapult.GetPoint(_maxLengh); // 제한거리 위치 얻음.
            }


            mouseWorldPoint.z = 0f;

            // 스톤의 위치를 마우스 클릭 지점으로 옮긴다 
            // spring joint component가 없을때만, 즉 쏘기전에만 
            if (_spring != null)
                transform.position = mouseWorldPoint;
        }

        if (_spring != null)
        {
            // 바로 전 속력이 크다는 것은 감속이 시작되었다고 인지할 수 있다.
            if (_prev_velocity.sqrMagnitude > _rb2d.velocity.sqrMagnitude)
            {
                Destroy(_spring); // spring 제거
                _springDestroy = true;
                _rb2d.velocity = _prev_velocity; // 마지막 속력값을 지정.
                deleteLine(); // catapult line 제거 
            }

            if (clickedOnBomb == false) _prev_velocity = _rb2d.velocity;
        }

        // catapult line
        UpdateLine();
    }

    void OnMouseDown()
    {
        clickedOnBomb = true;
    }

    void OnMouseUp()
    {
        clickedOnBomb = false;

        // rigidbody가 자신의 일을 하도록 풀어준다
        _rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ColliderTrigger) return; // OnCollisionEnter2D가 이미 한번 호출된 상태면 더이상 호출되지않음

        ColliderTrigger = true;
        StartCoroutine(DestroyBomb());
    }

    private IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(5f);
        //gameObject.SetActive(false); // 현재 Bomb disable

        // 현재 마지막 Bomb이었다면 
        // 새로운 Bomb를 소환한후 nextBomb에 연결 
        if (nextBomb == null)
        {
            //Vector3 pos = new Vector3(-13.95f, -0.49f, 0f);
            GameObject nextBombObject =
                Instantiate(preBomb, preBomb.transform.position, Quaternion.identity);
            nextBomb = nextBombObject;
        }

        if (nextBomb != null) // nextBomb이 있을때만
        {
            nextBomb.SetActive(true); // 다음 Bomb active

            // 카메라도 다음 Bomb로 이동
            Camera.main.GetComponent<CameraFollowBomb>().SwitchBomb();

            //
            if (ManageAngry.instAM.gameMode != 2)
            {
                ManageAngry.instAM.enemiesDead = 0;

                // 소환한 woodStructure, bird 모두 파괴 
                GameObject[] destroyObjects = GameObject.FindGameObjectsWithTag("SpawnedObject");
                foreach (GameObject x in destroyObjects)
                    Destroy(x);

                // 새로운 WoodStructure 생성         
                ManageAngry.instAM.Spawn();
            }
        }

        Destroy(gameObject); // 현재 Bomb 파괴
    }

    // catapult line 
    void UpdateLine()
    {
        if (!_isShowLine) return;

        // 라인의 2번째 지점을 Stone 위치로 계속 갱신
        _lineback.SetPosition(1, transform.position);
        _linefore.SetPosition(1, transform.position);
    }

    // 돌이 catapult 떠나면 line 제거
    void deleteLine()
    {
        _isShowLine = false;
        _lineback.enabled = false;
        _linefore.enabled = false;
    }

    // 새로운 폭탄 활성화시 cataplut line도 다시 활성화 
    void createLine()
    {
        _isShowLine = true;
        _lineback.enabled = true;
        _linefore.enabled = true;
    }
}
using UnityEngine;

namespace Management
{
    // abstract mathod를 가지는 클래스는 명시적으로 abstract class 가 되어야 한다.
    public abstract class Manage : MonoBehaviour
    {
        private GameObject _fadeobj;    // fade 프리펩 인스턴스 게체 참조할 변수
        private int _fadeSiblingInex;   // fade 인스턴스를 최상위 위치로 유지할 목적

        protected virtual void Awake()  // 자식 클래스 오버라이드 예정
        {
            _fadeobj = InstantiateUI("Fade", "Canvas", true);
            _fadeSiblingInex = _fadeobj.transform.GetSiblingIndex();
        }

        // pn = prefab name, cn = canvas name
        public GameObject InstantiateUI(string pn, string cn, bool isfull)
        {
            GameObject resource = (GameObject)Resources.Load(pn);
            GameObject obj = Instantiate(resource, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(GameObject.Find(cn).transform); // 부모 개체 지정

            if (isfull)
                ((RectTransform)obj.transform).offsetMax = Vector2.zero;
            else
                ((RectTransform)obj.transform).anchoredPosition = Vector2.zero;

            if (!pn.Equals("Fade"))
                obj.transform.SetSiblingIndex(_fadeSiblingInex);

            return obj;
        }

        // 페이드 아웃 / 다음씬 이동
        public void SetFadeout(int nextScene)
        {
            // 페이드아웃 작동
            _fadeobj.GetComponent<Fade>().setNextScene(nextScene);
            _fadeobj.SetActive(true);
            _fadeobj.GetComponent<Fade>().setFadeout();
        }

        // abstract 추상 함수, 반드시 자식 클래스에서 재정의 필요
        public abstract void SetStart();
    }
}
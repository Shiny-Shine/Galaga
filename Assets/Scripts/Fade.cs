using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    private int _nextScene = 0; // Fadeout 끝에서 넘어가야 할 씬 번호

    public void endFadeIn()
    {
        gameObject.SetActive(false);
    }

    // aniFadeout 클립 마지막 프레임에서의 이벤트 함수
    public void endFadeout()
    {
        SceneManager.LoadScene(_nextScene);
    }

    // aniFadeout 블럭으로 전이되도록 트리거 작동
    public void setFadeout()
    {
        GetComponent<Animator>().SetTrigger("SetFadeout");
    }

    // Fadeout 끝에서 넘어갈 씬의 번호 설정
    public void setNextScene(int _idx)
    {
        _nextScene = _idx;
    }

}
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    private int _nextScene = 0; // Fadeout ������ �Ѿ�� �� �� ��ȣ

    public void endFadeIn()
    {
        gameObject.SetActive(false);
    }

    // aniFadeout Ŭ�� ������ �����ӿ����� �̺�Ʈ �Լ�
    public void endFadeout()
    {
        SceneManager.LoadScene(_nextScene);
    }

    // aniFadeout ������ ���̵ǵ��� Ʈ���� �۵�
    public void setFadeout()
    {
        GetComponent<Animator>().SetTrigger("SetFadeout");
    }

    // Fadeout ������ �Ѿ ���� ��ȣ ����
    public void setNextScene(int _idx)
    {
        _nextScene = _idx;
    }

}
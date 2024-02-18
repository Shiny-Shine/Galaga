
using UnityEngine;
using Michsky.MUIP;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public TMP_Text selected;
    public int scenenum;
    public GameObject login, logout;
    public void btnSite(string url)
    {
        Application.OpenURL(url);
    }

    public void btnNoti(NotificationManager obj)
    {
        obj.Open();
    }

    public void btnGame(string name)
    {
        selected.text = name;
        if (name == "Flappy Bird")
            scenenum = 2;
        else if (name == "Galaga")
            scenenum = 1;
    }

    public void btnLogin(TMP_InputField userid)
    {
        MngApp.appInst.changeUser(userid.text);
        login.SetActive(false);
        logout.SetActive(true);
    }

    public void btnLogout()
    {
        MngApp.appInst.changeUser("none");
        login.SetActive(true);
        logout.SetActive(false);
    }

    public void btnStart()
    {
        SceneManager.LoadScene(scenenum);
    }
}

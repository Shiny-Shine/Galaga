
using System;
using UnityEngine;
using Michsky.MUIP;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public TMP_Text selected, txtName, txtBestF, txtBestA, txtBestG;
    public NotificationManager notiLoginFail;
    public int scenenum = 0;
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
        else if (name == "Angry Bird")
            scenenum = 1;
    }

    public void btnLogin(TMP_InputField userid)
    {
        if (userid.text.Length < 3)
        {
            btnNoti(notiLoginFail);
            return;
        }
        login.SetActive(false);
        logout.SetActive(true);
        txtName.text = userid.text;
        MngApp.appInst.loginUser = userid.text;
    }

    public void btnLogout()
    {
        MngApp.appInst.changeUser("none");
        login.SetActive(true);
        logout.SetActive(false);
    }

    public void btnStart(NotificationManager obj)
    {
        if(scenenum == 0)
            return;
        if (login.activeSelf)
        {
            btnNoti(obj);
            return;
        }
        SceneManager.LoadScene(scenenum);
    }
}

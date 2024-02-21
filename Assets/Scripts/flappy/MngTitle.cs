using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MngTitle : MonoBehaviour {
    private GameObject pInput;
    private TMP_Text tName, tBest, btnName;
    private TMP_InputField ifInput;
    private bool ifShow = false;

    void Start () {
        referObject ();
        MngApp.appInst.changeUser(MngApp.appInst.loginUser);
        updateData ();
    }

    void referObject () {
        pInput = GameObject.Find ("InputField (TMP)");
        ifInput = pInput.GetComponent<TMP_InputField> ();
        tName = GameObject.Find ("txtName").GetComponent<TMP_Text> ();
        tBest = GameObject.Find ("txtBest").GetComponent<TMP_Text> ();
        btnName = GameObject.Find ("btnTextName").GetComponent<TMP_Text> ();
    }

    void updateData () {
        pInput.SetActive (ifShow);
        tName.text = "Name : " + MngApp.appInst.Name;
        tBest.text = "Best : " + MngApp.appInst.Best;
    }

    public void onClickName () {
        ifShow = !ifShow;
        btnName.text = (ifShow) ? "Ok" : "Name";

        if (!ifShow) {
            string _name = ifInput.text;
            if (_name != "") MngApp.appInst.changeUser (_name);
            ifInput.text = "";
        }

        updateData ();
        if (ifShow) ifInput.Select ();
    }

    public void onClickStart () {
        if (MngApp.appInst.Name=="none") return;
        if (ifShow) return;
        SceneManager.LoadScene ("FlappyGame");
    }

    public void onClickReturn()
    {
        SceneManager.LoadScene ("Title");
    }
}
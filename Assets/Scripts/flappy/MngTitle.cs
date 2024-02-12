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
        tName.text = "Name : " + MngApp.inst.Name;
        tBest.text = "Best : " + MngApp.inst.Best;
    }

    public void onClickName () {
        ifShow = !ifShow;
        btnName.text = (ifShow) ? "Ok" : "Name";

        if (!ifShow) {
            string _name = ifInput.text;
            if (_name != "") MngApp.inst.changeUser (_name);
            ifInput.text = "";
        }

        updateData ();
        if (ifShow) ifInput.Select ();
    }

    public void onClickStart () {
        if (MngApp.inst.Name=="none") return;
        if (ifShow) return;
        SceneManager.LoadScene ("FlappyGame");
    }
}
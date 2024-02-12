using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MngTitle : MonoBehaviour {
    private GameObject pInput;
    private Text tName, tBest, btName;
    private InputField ifInput;
    private bool ifShow = false;

    void Start () {
        referObject ();
        updateData ();
    }

    void referObject () {
        pInput = GameObject.Find ("InputField");
        ifInput = pInput.GetComponent<InputField> ();
        tName = GameObject.Find ("tName").GetComponent<Text> ();
        tBest = GameObject.Find ("tBest").GetComponent<Text> ();
        btName = GameObject.Find ("btName").GetComponent<Text> ();
    }

    void updateData () {
        pInput.SetActive (ifShow);
        tName.text = "Name : " + MngApp.inst.Name;
        tBest.text = "Best : " + MngApp.inst.Best;
    }

    public void onClickName () {
        ifShow = !ifShow;
        btName.text = (ifShow) ? "Okay" : "Name";

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
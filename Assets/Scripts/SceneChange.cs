using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("GalagaGame");
    }
    
    public void ReturnBtn()
    {
        SceneManager.LoadScene("GalagaTitle");
    }

    public void mainReturn()
    {
        SceneManager.LoadScene("Title");
    }
}

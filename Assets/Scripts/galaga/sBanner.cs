using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class sBanner : MonoBehaviour
{
    public TMP_Text txt;
    void Start()
    {
        txt = gameObject.GetComponent<TMP_Text>();
        txt.text = String.Format("Stage {0}", GameManager.instance.Stage);
    }

    private void OnEnable()
    {
        txt = gameObject.GetComponent<TMP_Text>();
        txt.text = String.Format("Stage {0}", GameManager.instance.Stage);
        Invoke("disable", 3f);
    }

    void disable()
    {
        gameObject.SetActive(false);
    }
}

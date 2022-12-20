using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil_Pressure_Level_Warning : MonoBehaviour
{
    public GameObject button_Abnormal;
    public GameObject button_Normal;

    void Start()
    {
        button_Abnormal.SetActive(false);
        button_Normal.SetActive(false);
    }

    public void Normal()
    {
        button_Normal.SetActive(true);
        button_Abnormal.SetActive(false);
    }

    public void Abnormal()
    {
        button_Normal.SetActive(false);
        button_Abnormal.SetActive(true);
    }
}

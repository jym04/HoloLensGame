using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Text : MonoBehaviour
{
    //private GameObject mqtt;
    public Text dataValue;
    private GameObject textManager;

    private void Start()
    {
        //mqtt = GameObject.Find("M2MQTT");
        textManager = GameObject.Find("UI_TextManager");
    }
    /*
    void Update()
    {
        //dataValue.text = mqtt.GetComponent<M2Mqtt>().log1.ToString(); //¾ÈµÊ
        //textManager.GetComponent<UI_TextManager>().TextCopy(dataValue, this.name);
    }
    */
}

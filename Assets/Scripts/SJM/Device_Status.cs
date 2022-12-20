using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device_Status : MonoBehaviour
{
    public GameObject greenButton_1;
    public GameObject greenButton_2;
    public GameObject grayButton_1;
    public GameObject grayButton_2;

    void Start()
    {
        greenButton_1.SetActive(false);
        greenButton_2.SetActive(false);
        grayButton_1.SetActive(true);
        grayButton_2.SetActive(true);
    }

    public void Value_0()
    {
        greenButton_1.SetActive(false);
        greenButton_2.SetActive(false);
        grayButton_1.SetActive(true);
        grayButton_2.SetActive(true);
    }

    public void Value_1()
    {
        greenButton_1.SetActive(true);
        greenButton_1.SetActive(false);
        grayButton_1.SetActive(false);
        grayButton_2.SetActive(true);
    }

    public void Value_2()
    {
        greenButton_1.SetActive(true);
        greenButton_1.SetActive(true);
        grayButton_1.SetActive(false);
        grayButton_2.SetActive(false);
    }
}

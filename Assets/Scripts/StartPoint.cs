using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartPoint : MonoBehaviour
{
    public bool isStart;

    public TMP_Text errorNum;
    public TMP_Text debugText;

    public void DebugingText()
    {
        errorNum.text = "Error Code : 001";
        debugText.text = "Player 생성 지점에 장애물이" + System.Environment.NewLine + " 생성되어 있습니다";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            isStart = false;
        }
    }
}

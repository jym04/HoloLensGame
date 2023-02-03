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
        debugText.text = "Player ���� ������ ��ֹ���" + System.Environment.NewLine + " �����Ǿ� �ֽ��ϴ�";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            isStart = false;
        }
    }
}

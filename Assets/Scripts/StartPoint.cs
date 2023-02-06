using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BoxPointType
{
    Spawn,
    Arrive
}
public class StartPoint : MonoBehaviour
{
    public GameButtonManager gameManager;

    private string errorNum;
    private string debug;

    public TMP_Text errorNumText;
    public TMP_Text debugText;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameButtonManager>();
    }

    public void DebugingText()
    {
        errorNumText.text = errorNum;
        debugText.text = debug;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("QRCode"))
        {
            gameManager.isStart = false;

            debug = "Player ����/���� ������ " + System.Environment.NewLine + "��ֹ� ������ �õ� �Ͽ����ϴ�.";
            errorNum = "Error Code : 001";

            DebugingText();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("QRCode"))
        {
            gameManager.isStart = true;
        }
    }
}

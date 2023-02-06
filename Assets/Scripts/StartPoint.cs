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

            debug = "Player 생성/도착 지점에 " + System.Environment.NewLine + "장애물 생성을 시도 하였습니다.";
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

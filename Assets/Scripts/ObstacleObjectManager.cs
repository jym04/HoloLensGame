using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTracking;
using TMPro;

public class ObstacleObjectManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject[] O_obstaclePrefab;
    public GameObject[] p_obstaclePrefab;
    public GameObject obstaclesPlace;
    public GameObject obstaclesCollection;

    public GameButtonManager gameManager;
    public QRCodesVisualizer qrCodesVisualizer;
    public GameObject[] testQRCodePosition;

    public GameObject[] spawnPoint;

    //public TMP_Text qrcodelist; //디버그 테스트
    public void InstantiateObjects()
    {
        if (gameManager.gameType == GameType.ObstacleAvoidance)
        {

            for (int i = 0; i < spawnPoint.Length; i++)
            {
                GameObject obstacle = Instantiate(O_obstaclePrefab[i],spawnPoint[i].transform.position,obstaclesPlace.transform.rotation, obstaclesCollection.transform);
            }
        }
        else if (gameManager.gameType == GameType.PathPlanning)
        {
            //Unity Editor 용 코드
            //foreach(var qrcode in testQRCodePosition)
            //{
            //    Instantiate(obstaclePrefab, qrcode.gameObject.transform.position, Quaternion.identity, obstaclesCollection.transform);
            //}

            //HoloLens 용 코드
            foreach (var qrcode in qrCodesVisualizer.qrCodesObjectsList)
            {
                if (qrcode.Key.Contains("Pile Of Box"))
                {
                    obstaclePrefab = p_obstaclePrefab[0];
                }
                else if (qrcode.Key.Contains("Sign"))
                {
                    obstaclePrefab = p_obstaclePrefab[1];
                }
                else if(qrcode.Key.Contains("Pile Of DrumBox"))
                {
                    obstaclePrefab = p_obstaclePrefab[2];
                }
                else
                {
                    obstaclePrefab = O_obstaclePrefab[3];
                }
                GameObject obstacle = Instantiate(obstaclePrefab, 
                    new Vector3(qrcode.Value.gameObject.transform.localPosition.x, qrcode.Value.gameObject.transform.localPosition.y+0.05f, qrcode.Value.gameObject.transform.localPosition.z),
                    Quaternion.Euler(0, qrcode.Value.gameObject.transform.localRotation.eulerAngles.y + qrcode.Value.gameObject.transform.localRotation.eulerAngles.z, 0), obstaclesCollection.transform) ;
                //qrcodelist.text = "qrcode Count : " + qrCodesVisualizer.qrCodesObjectsList.Count;
            }
        }
    }
    public void DeleteObjects()
    {
        Transform[] childList = obstaclesCollection.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for(int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }
    }
    public void ResetObjects()
    {
        DeleteObjects();
        InstantiateObjects();
    }
}

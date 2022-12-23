using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTracking;
using TMPro;

public class ObstacleObjectManager : MonoBehaviour
{
    public float x_Start, y_Start, z_Start;
    public int columnLength, rowLength;
    public float x_Space, y_Space, z_Space;
    public GameObject obstaclePrefab;
    public GameObject obstaclesPlace;
    public GameObject obstaclesCollection;

    public GameButtonManager gameManager;
    public QRCodesVisualizer qrCodesVisualizer;
    public GameObject[] testQRCodePosition;

    public TMP_Text qrcodelist;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InstantiateObjects()
    {
        if (gameManager.gameType == GameType.DodgerGame)
        {
            for (int i = 0; i < columnLength; i++)
            {
                GameObject obstacle = Instantiate(obstaclePrefab,
                    new Vector3(obstaclesPlace.transform.position.x + x_Start + x_Space * (i % columnLength),
                    obstaclesPlace.transform.position.y + y_Start + y_Space * (i % columnLength),
                    obstaclesPlace.transform.position.z + z_Start + z_Space * (i % columnLength)),
                    Quaternion.identity, obstaclesCollection.transform);
            }
        }
        else if (gameManager.gameType == GameType.MazeRunnerGame)
        {
            
            //foreach(var qrcode in testQRCodePosition)
            //{
            //    Instantiate(obstaclePrefab, qrcode.gameObject.transform.position, Quaternion.identity, obstaclesCollection.transform);
            //}
            foreach (var qrcode in qrCodesVisualizer.qrCodesObjectsList)
            {
                Instantiate(obstaclePrefab, new Vector3(qrcode.Value.gameObject.transform.localPosition.x, qrcode.Value.gameObject.transform.localPosition.y+0.02f, qrcode.Value.gameObject.transform.localPosition.z), Quaternion.identity, obstaclesCollection.transform);
                qrcodelist.text = "qrcode Count : " + qrCodesVisualizer.qrCodesObjectsList.Count;
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

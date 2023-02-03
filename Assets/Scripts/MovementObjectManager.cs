using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovementObjectManager : MonoBehaviour
{
    public GameButtonManager gameButtonManager;

    public GameObject moveObjectPrefab;

    public GameObject movementObjectCollection;

    public Transform obstacleAvoidance_spawnPoint;
    public Transform pathPlanning_spawnPoint;

    public int spawnCount;
    public int arriveCount;

    public TMP_Text obstacleAvoidance_spawnCountText;
    public TMP_Text obstacleAvoidance_arriveCountText;

    public TMP_Text pathPlanning_spawnCountText;
    public TMP_Text pathPlanning_arriveCountText;

    private void Update()
    {
        obstacleAvoidance_spawnCountText.text = spawnCount.ToString();
        obstacleAvoidance_arriveCountText.text = arriveCount.ToString();

        pathPlanning_spawnCountText.text = spawnCount.ToString();
        pathPlanning_arriveCountText.text = arriveCount.ToString();
    }
    public void SpawnObject()
    {
        if (gameButtonManager.gameType == GameType.ObstacleAvoidance)
        {
            Vector3 spawnPosition = new Vector3(obstacleAvoidance_spawnPoint.position.x, obstacleAvoidance_spawnPoint.position.y + 0.02f, obstacleAvoidance_spawnPoint.position.z);
            GameObject moveObject = Instantiate(moveObjectPrefab, spawnPosition, Quaternion.identity, movementObjectCollection.transform);
            spawnCount += 1;
        }
        else
        {
            Vector3 spawnPosition = new Vector3(pathPlanning_spawnPoint.position.x, pathPlanning_spawnPoint.position.y + 0.02f, pathPlanning_spawnPoint.position.z);
            GameObject moveObject = Instantiate(moveObjectPrefab, spawnPosition, Quaternion.identity, movementObjectCollection.transform);
            spawnCount += 1;
        }
        
    }
    public void DeleteObjects()
    {
        Transform[] childList = movementObjectCollection.GetComponentsInChildren<Transform>();

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
        spawnCount = 0;
        arriveCount = 0;
    }
}

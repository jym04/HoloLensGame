using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovementObjectManager : MonoBehaviour
{
    public GameButtonManager gameButtonManager;

    public GameObject moveObjectPrefab;

    public GameObject movementObjectCollection;

    public Transform dodger_spawnPoint;
    public Transform maze_spawnPoint;

    public int spawnCount;
    public int arriveCount;

    public TMP_Text dodger_spawnCountText;
    public TMP_Text dodger_arriveCountText;

    public TMP_Text maze_spawnCountText;
    public TMP_Text maze_arriveCountText;

    private void Update()
    {
        dodger_spawnCountText.text = spawnCount.ToString();
        dodger_arriveCountText.text = arriveCount.ToString();

        maze_spawnCountText.text = spawnCount.ToString();
        maze_arriveCountText.text = arriveCount.ToString();
    }
    public void SpawnObject()
    {
        if (gameButtonManager.gameType == GameType.DodgerGame)
        {
            Vector3 spawnPosition = new Vector3(dodger_spawnPoint.position.x, dodger_spawnPoint.position.y + 0.02f, dodger_spawnPoint.position.z);
            GameObject moveObject = Instantiate(moveObjectPrefab, spawnPosition, Quaternion.identity, movementObjectCollection.transform);
            spawnCount += 1;
        }
        else
        {
            Vector3 spawnPosition = new Vector3(maze_spawnPoint.position.x, maze_spawnPoint.position.y + 0.02f, maze_spawnPoint.position.z);
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

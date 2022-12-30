using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovementObjectManager : MonoBehaviour
{
    public GameObject moveObjectPrefab;
    public GameObject movementObjectCollection;
    public Transform spawnPoint;

    public int spawnCount;
    public int arriveCount;

    public TMP_Text spawnCountText;
    public TMP_Text arriveCountText;

    private void Update()
    {
        spawnCountText.text = spawnCount.ToString();
        arriveCountText.text = arriveCount.ToString();
    }
    public void SpawnObject()
    {
        Vector3 spawnPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y + 0.02f, spawnPoint.position.z);
        GameObject moveObject = Instantiate(moveObjectPrefab, spawnPosition, Quaternion.identity, movementObjectCollection.transform);
        spawnCount += 1;
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

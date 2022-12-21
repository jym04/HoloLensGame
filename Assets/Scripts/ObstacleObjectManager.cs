using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectManager : MonoBehaviour
{
    public float x_Start, y_Start, z_Start;
    public int columnLength, rowLength;
    public float x_Space, y_Space, z_Space;
    public GameObject obstaclePrefab;
    public GameObject obstaclesPlace;
    public GameObject obstaclesCollection;

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
        for(int i = 0; i < columnLength; i++)
        {
            Instantiate(obstaclePrefab,
                new Vector3(obstaclesPlace.transform.position.x + x_Start + x_Space * (i % columnLength),
                obstaclesPlace.transform.position.y + y_Start + y_Space * (i % columnLength),
                obstaclesPlace.transform.position.z + z_Start + z_Space * (i % columnLength)),
                Quaternion.identity, obstaclesCollection.transform);
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

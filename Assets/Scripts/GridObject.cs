using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public float x_Start, y_Start, z_Start;
    public int columnLength, rowLength;
    public float x_Space, y_Space, z_Space;
    public GameObject prefab;
    public GameObject gridCollection;

    // Start is called before the first frame update
    void Start()
    {
        GridSetObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GridSetObjects()
    {
        for(int i = 0; i < columnLength; i++)
        {
            Instantiate(prefab, 
                new Vector3(x_Start + x_Space * (i % columnLength), y_Start + y_Space * (i % columnLength), z_Start+z_Space*(i%columnLength)), 
                Quaternion.identity);
        }
    }
}

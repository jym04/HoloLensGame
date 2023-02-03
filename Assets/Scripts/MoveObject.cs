using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject movementObjectManager;

    public Vector3[] spawnWayPoint;
    public Vector3[] arriveWayPoint;

    public Vector3 currentPosition;

    public GameObject test;

    private int spawnWayPointIndex = 0;
    public int arriveWayPointIndex = 0;

    public GameObject spawnWayPointObjects;
    public GameObject arriveWayPointObjects;

    public GameObject map;

    public bool insert;
    public bool remove;

    Quaternion rotation;

    private void Start()
    {
        movementObjectManager = GameObject.FindWithTag("SpawnObject");
        spawnWayPointObjects = GameObject.Find("SpawnObjectMovePoints");
        arriveWayPointObjects = GameObject.Find("ArriveObjectMovePoints");
        map = GameObject.FindWithTag("Map");
        test = GameObject.Find("StartPoint");

        spawnWayPoint = new Vector3[spawnWayPointObjects.transform.childCount];
        arriveWayPoint = new Vector3[arriveWayPointObjects.transform.childCount];

        for (int i = 0; i < spawnWayPointObjects.transform.childCount; i++)
        {
            spawnWayPoint[i] = spawnWayPointObjects.transform.GetChild(i).transform.position;
        }
        for (int i = 0; i < arriveWayPointObjects.transform.childCount; i++)
        {
            arriveWayPoint[i] = arriveWayPointObjects.transform.GetChild(i).transform.position;
        }
    }
    private void Update()
    {
        ConveyorBeltMove();
    }
    private void ConveyorBeltMove()
    {
        if (!insert)
        {
            currentPosition = transform.position;

            if (spawnWayPointIndex < spawnWayPoint.Length)
            {
                float step = Time.deltaTime * 0.3f;

                transform.position = Vector3.MoveTowards(currentPosition, spawnWayPoint[spawnWayPointIndex], step);
                transform.LookAt(spawnWayPoint[spawnWayPointIndex]);

                if (Mathf.Abs(Vector3.Distance(spawnWayPoint[spawnWayPointIndex], currentPosition)) <= 0.01f)
                {
                    spawnWayPointIndex++;
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, map.transform.rotation.eulerAngles.y + 90, 0));
            }
            
        }
        else if (remove)
        {
            currentPosition = transform.position;

            if (arriveWayPointIndex < arriveWayPoint.Length)
            {

                float step = Time.deltaTime * 0.3f;

                transform.position = Vector3.MoveTowards(currentPosition, arriveWayPoint[arriveWayPointIndex], step);
                Vector3 lookRotation = arriveWayPoint[arriveWayPointIndex] - transform.position;

                rotation = Quaternion.LookRotation(lookRotation);

                rotation.x = 0f;
                rotation.z = 0f;

                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.fixedDeltaTime * 15);

                if (Mathf.Abs(Vector3.Distance(arriveWayPoint[arriveWayPointIndex], currentPosition)) <= 0.01f)
                {
                    arriveWayPointIndex++;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void ArriveSetObject()
    {
        gameObject.GetComponent<MoveObject>().remove = true;
        transform.position = arriveWayPoint[0];
        transform.parent = movementObjectManager.transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, map.transform.rotation.eulerAngles.y - 90f, 0);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            insert = true;

            GameObject movementObjectCollection = collision.gameObject.transform.GetChild(5).gameObject;
            if (movementObjectCollection.transform.childCount == 0)
            {
                gameObject.transform.parent = movementObjectCollection.transform;
                gameObject.transform.position = movementObjectCollection.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(0, map.transform.rotation.eulerAngles.y + 90f, 0);
            }
        }
    }
}

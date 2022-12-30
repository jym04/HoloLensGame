using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject movementObjectCollection = collision.gameObject.transform.GetChild(5).gameObject;
            if (movementObjectCollection.transform.childCount==0)
            {
                gameObject.transform.parent = collision.gameObject.transform.GetChild(5);
                gameObject.transform.position = collision.gameObject.transform.GetChild(5).position;
            }
        }
    }
}

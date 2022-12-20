using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{
    float rotateSpeed = 20;

    void OnMouseDrag()
    {
        float rotateX = Input.GetAxis("Mouse X") * rotateSpeed * Mathf.Deg2Rad;
        float rotateY = Input.GetAxis("Mouse Y") * rotateSpeed * Mathf.Deg2Rad;

        transform.RotateAround(Vector3.up, -rotateX);
        transform.RotateAround(Vector3.right, rotateY);
    }
}

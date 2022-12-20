using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotration = Vector3.up;
    [SerializeField]
    private float _speed = 1;

    private void Update()
    {
        transform.Rotate(_rotration * _speed * Time.deltaTime);
    }
}

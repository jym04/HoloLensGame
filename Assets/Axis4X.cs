using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis4X : MonoBehaviour
{
    public void RotateAxis4X(float targetRotationValue, float smooth)
    {
        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(targetRotationValue, 0, 0);
        // Dampen towards the target rotation
        transform.localRotation = Quaternion.Slerp(transform.rotation, target, smooth);
    }
}

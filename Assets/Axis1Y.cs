using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis1Y : MonoBehaviour
{
    public void RotateAxis1Y(float targetRotationValue, float smooth)
    {

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(0, targetRotationValue, 0);
        // Dampen towards the target rotation
        transform.localRotation = Quaternion.Slerp(transform.rotation, target,  smooth);
        //transform.Rotate(new Vector3(0, targetRotationValue * Time.deltaTime, 0));

    }

}

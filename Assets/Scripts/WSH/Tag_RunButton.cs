using System;
using UnityEngine;

namespace WSH
{
    [RequireComponent(typeof(MeshCollider))]
    public class Tag_RunButton : MonoBehaviour
    {
        internal Action triggerEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(ControlPanel.controllerTag))
                return;

            triggerEvent();
        }
    }
}
using System;
using UnityEngine;

namespace WSH
{
    [RequireComponent(typeof(MeshCollider))]
    public class Tag_EmergencyButton : MonoBehaviour {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(ControlPanel.controllerTag))
                return;

        }

        internal Action triggerEvent;
    }
}
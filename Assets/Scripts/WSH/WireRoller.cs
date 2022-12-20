using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WSH.Animator
{
    public class WireRoller : MonoBehaviour
    {
        [Range(0, 120f)]
        public float speed;
        [SerializeField] Transform[] rollers;
        private void Awake()
        {
            rollers = GetComponentsInChildren<Transform>().Where(g => g.CompareTag(Tags.Roll.ToString())).ToArray();
        }

        private void Update()
        {
            var value = Time.deltaTime * speed;
            foreach (var roller in rollers)
            {
                var rot = roller.rotation.eulerAngles;
                rot.z += value;
                roller.rotation = Quaternion.Euler(rot);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Tags
{
    Roll,
}

namespace WSH.Animator
{
    public class Wire : MonoBehaviour
    {
        public float rollingSpeed;
        [SerializeField] Transform[] rollers;
        public void Init(float speed)
        {
            rollingSpeed = speed;
            //foreach(var r in rollers)
            //{
            //    var rot = r.rotation.eulerAngles;
            //    rot.z = Random.Range(0, 360f);
            //    r.rotation = Quaternion.Euler(rot);
            //}
        }

        bool isOn;

        public void RoliingStart()
        {
            isOn = true;
            StartCoroutine(Rolling());
        }

        public void RollingStop()
        {
            isOn = false;
        }

        IEnumerator Rolling()
        {
            var value = Time.fixedDeltaTime* rollingSpeed;
            while (isOn)
            {
                foreach(var roller in rollers)
                {
                    var rot = roller.rotation.eulerAngles;
                    rot.z += value;
                    roller.rotation = Quaternion.Euler(rot);
                    yield return null;
                }
            }
        }
    }
}
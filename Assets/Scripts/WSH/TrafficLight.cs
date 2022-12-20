using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public enum TrafficState
{
    Normal,
    Warning,
    Error,
}
namespace WSH
{
    public class TrafficLight : MonoBehaviour
    {
        public Light red;
        public Light green;
        public Light yellow;
        public Light currentTraffic;
        private void Awake()
        {
            var lights = GetComponentsInChildren<Light>();
            red = lights.Where(l => l.name.Equals("R")).First();
            green = lights.Where(l => l.name.Equals("G")).First();
            yellow = lights.Where(l => l.name.Equals("Y")).First();
            red.gameObject.SetActive(false);
            green.gameObject.SetActive(false);
            yellow.gameObject.SetActive(false);
            SetState(TrafficState.Normal);
        }

        public void SetState(TrafficState state)
        {
            if (currentTraffic != null)
                currentTraffic.gameObject.SetActive(false);
            switch (state)
            {
                case TrafficState.Normal:
                    currentTraffic = green;
                    break;
                case TrafficState.Warning:
                    currentTraffic = yellow;
                    break;
                case TrafficState.Error:
                    currentTraffic = red;
                    break;
            }
            currentTraffic.gameObject.SetActive(true);

            StopAllCoroutines();
            LightAnim();
        }
        public float speed;

        void LightAnim()
        {
            var light = currentTraffic.GetComponent<HDAdditionalLightData>();
            StopAllCoroutines();
            StartCoroutine(LightVolumetricOn(light));
        }
        IEnumerator LightVolumetricOn(HDAdditionalLightData light)
        {
            float prevDimmer = light.volumetricDimmer;
            while (prevDimmer < 16f)
            {
                prevDimmer = light.volumetricDimmer;
                float newDimmer;
                newDimmer = prevDimmer + (Time.deltaTime * speed);
                light.SetLightDimmer(light.lightDimmer, newDimmer);
                yield return null;
            }
            StartCoroutine(LightVolumetricOff(light));
        }
        IEnumerator LightVolumetricOff(HDAdditionalLightData light)
        {
            float prevDimmer = light.volumetricDimmer;
            while (prevDimmer > 0f)
            {
                prevDimmer = light.volumetricDimmer;
                float newDimmer;
                newDimmer = prevDimmer - (Time.deltaTime * speed);
                light.SetLightDimmer(light.lightDimmer, newDimmer);
                yield return null;
            }
            StartCoroutine(LightVolumetricOn(light));
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using WIFramework.UI;

namespace WIFramework.Core.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] WIBehaviour[] wiObjects;
        public void Start()
        {
            wiObjects = FindObjectsOfType<WIBehaviour>();
            foreach(var wi in wiObjects)
            {
                wi.Initialize();
            }
        }

        List<KeyCode> inputs = new List<KeyCode>();
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                inputs.Add(KeyCode.Q);
            if (Input.GetKeyDown(KeyCode.W))
                inputs.Add(KeyCode.W);
            if (Input.GetKeyDown(KeyCode.E))
                inputs.Add(KeyCode.E);

            if (inputs.Count > 0)
            {
                for (int i = 0; i < inputs.Count; ++i)
                {
                    foreach (var wi in wiObjects)
                    {
                        wi.ActionTest(inputs[i]);
                    }
                }
                inputs.Clear();
            }
        }
    }
}
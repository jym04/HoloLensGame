using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSH.UI;

namespace WSH
{
    public class ControlPanel : MonoBehaviour
    {
        public static string controllerTag = "Hand";
        [SerializeField] Tag_EmergencyButton button_Emergency;
        [SerializeField] Tag_RunButton button_Run;

        [SerializeField] UI_Panel_PermissionCheck panel_PermissionCheck;
        [SerializeField] UI_Panel_Run panel_Run;
        private void Awake()
        {
            button_Emergency = GetComponentInChildren<Tag_EmergencyButton>();
            button_Run = GetComponentInChildren<Tag_RunButton>();
            button_Emergency.triggerEvent = OnClick_Emergency;
            button_Run.triggerEvent = OnClick_Runing;

            panel_PermissionCheck = GetComponentInChildren<UI_Panel_PermissionCheck>();
            panel_Run = GetComponentInChildren<UI_Panel_Run>();
        }

        void OnClick_Emergency()
        {
            Debug.Log($"OnClickEvent:ControlPanelEmergencyStop");
            if (panel_PermissionCheck.gameObject.activeSelf)
            {
                return;
            }
            panel_PermissionCheck.Active();
        }

        void OnClick_Runing()
        {
            Debug.Log($"OnClickEvent:ControlPanelRun");
            if (panel_Run.gameObject.activeSelf)
            {
                return;
            }
            panel_Run.gameObject.SetActive(true);
        }
    }
}
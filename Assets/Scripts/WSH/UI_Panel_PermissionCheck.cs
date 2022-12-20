using UnityEngine;
using UnityEditor;
using WIFramework.UI;
using UnityEngine.UI;
using System;
using System.Collections;

namespace WSH.UI
{
    public class UI_Panel_PermissionCheck : PanelBase
    {
        public Button button_Ok;
        public Button button_Cancel;
        public Image Panel_PermissionCheck;
        public UI_Panel_PermissionDenied Panel_PermissionDenied;
        public float fadingTime;
        public override void Initialize()
        {
            button_Ok.onClick.AddListener(OnClick_Ok);
            button_Cancel.onClick.AddListener(Deactive);

            GetPanel(out Panel_PermissionDenied);
            //Panel_PermissionDenied.button_Close.onClick.AddListener(Deactive);
            Deactive();
        }

        public override void Active()
        {
            if (gameObject.activeSelf)
                return;
            Debug.Log("Active=UI_Panel_PermissionCheck");
            base.Active();
            StopAllCoroutines();
            StartCoroutine(BackgroundFadeIn());
        }

        public override void Deactive()
        {
            if (!gameObject.activeSelf)
                return;

            Debug.Log("Deactive=UI_Panel_PermissionCheck");
            Panel_PermissionDenied.Deactive();
            gameObject.SetActive(false);
        }
        public override void ActionTest(KeyCode key)
        {
            
            switch (key)
            {
                case KeyCode.Q:
                    Active();
                    break;
                case KeyCode.W:
                    if (!gameObject.activeSelf)
                        return;

                    OnClick_Ok();
                    break;
                case KeyCode.E:
                    if (!gameObject.activeSelf)
                        return;

                    Deactive();
                    break;
            }
        }
        public void OnClick_Ok()
        {
            Debug.Log("OnClick=Ok");
            StopAllCoroutines();
            Panel_PermissionDenied.Active();
            StartCoroutine(AutoDeactive());
        }

        private IEnumerator AutoDeactive()
        {
            timer = 0f;
            while (offTimer > timer)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            Deactive();
        }

        //public Button button_Close;
        public float offTimer = 2f;
        float timer = 0f;
        #region Anim
        IEnumerator BackgroundFadeOut()
        {
            float timer = fadingTime;
            var color = Panel_PermissionCheck.color;
            while (timer>=0f)
            {
                timer -= Time.deltaTime;
                color.a = timer / fadingTime;
                Panel_PermissionCheck.color = color;
                yield return null;
            }
            StartCoroutine(BackgroundFadeIn());
        }
        IEnumerator BackgroundFadeIn()
        {
            float timer = 0;
            var color = Panel_PermissionCheck.color;
            while (timer < fadingTime)
            {
                timer += Time.deltaTime;
                color.a = timer / fadingTime;
                Panel_PermissionCheck.color = color;
                yield return null;
            }
            StartCoroutine(BackgroundFadeOut());

        }
        #endregion
    }
}
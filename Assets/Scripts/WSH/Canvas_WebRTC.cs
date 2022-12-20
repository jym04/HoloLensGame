/* 
 * Copyright (C) 2021 because-why-not.com Limited
 * 
 * Please refer to the license.txt for license information
 */
using Byn.Awrtc.Unity;
using UnityEngine;
using UnityEngine.UI;
using WIFramework.UI;
/// <summary>
/// Modify 2022-11-07 -WSH-
/// </summary>
/// 
public enum SceneName
{
    conferencescene,
    menuscene
}
public class Canvas_WebRTC : WIBehaviour
{
    private static bool created = false;
    public RectTransform panel_StartMenu;
    public Button button_Menu;
    public Button button_Conference;

    private void Awake()
    {
        if (created)
        {
            Destroy(this.gameObject);
        }
        created = true;
        GetUIElement(nameof(button_Menu), out button_Menu);
        GetUIElement(nameof(button_Conference), out button_Conference);

        button_Menu.onClick.RemoveAllListeners();
        button_Menu.onClick.AddListener(OnClick_Button_Menu);

        button_Conference.onClick.RemoveAllListeners();
        button_Conference.onClick.AddListener(OnClick_Button_Conference);
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Active_UI_Menu();
    }

    private void PrintDeviceDebug()
    {
#if !UNITY_WEBGL && !UNITY_WSA
        var testFactory = new WebRtcCSharp.RTCPeerConnectionFactoryRef();
        Debug.Log(testFactory.GetDebugDeviceInfo());
        testFactory.Dispose();
#endif
    }
    public override void Initialize()
    {
        //just to trigger the init process
        var res = UnityCallFactory.Instance;
    }

    public void Deactive_UI_Menu()
    {
        button_Menu.gameObject.SetActive(true);
        panel_StartMenu.gameObject.SetActive(false);
    }

    public void Active_UI_Menu()
    {
        button_Menu.gameObject.SetActive(false);
        panel_StartMenu.gameObject.SetActive(true);
    }
    public void OnClick_Button_Conference()
    {
        Deactive_UI_Menu();
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName.conferencescene.ToString());
    }
    public void OnClick_Button_Menu()
    {
        Active_UI_Menu();
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName.menuscene.ToString());
    }
}

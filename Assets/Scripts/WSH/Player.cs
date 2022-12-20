using Byn.Awrtc;
using Byn.Awrtc.Unity;
using Byn.Unity.Examples;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WIFramework.UI;
public class VideoData
{
    public GameObject uiObject;
    public Texture2D texture;
    public RawImage image;
}

/// <summary>
/// Modify 2022-11-09 -WSH-
/// 1. 내화면 안나오는거 고치기->2022-11-10 수정완료
/// 2. 음성 테스트 하기
/// 3. 이동 동기화를 입력동기화로 바꾸기
/// </summary>
public class Player : WIBehaviour
{
    #region LocalDefine
    private const int MAX_CODE_LENGTH = 256;
    public static readonly string defaultName = "unknown";
    #endregion

    #region Variables
    public ICall myCall;
    public string userName;
    [SerializeField] public bool isDummy;
    InputField inputField_RoomName;
    Button button_Join;
    Button button_Shutdown;
    Toggle toggle_Video;
    Toggle toggle_Audio;
    WebRTCManager rtcManager;
    internal VideoData videoData;
    #endregion

    #region Properties
    public MediaConfig mediaConfig = new MediaConfig();
    public NetworkConfig netConfig = new NetworkConfig();
    #endregion

    #region UnityMethods
    private void Update()
    {
        if (myCall != null)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                OnClick_Button_Send();
            }
            //update the call
            myCall.Update();
        }
    }
    #endregion
    public void Initialize(bool isDummy)
    {
        if (isDummy)
            Debug.Log($"Dummy Player Initialize.");
        else
            Debug.Log($"Player Initialize.");
        rtcManager = FindObjectOfType<WebRTCManager>();
        //GetPanel(out panel_VideoLayout);

        gameObject.name = defaultName;
        this.isDummy = isDummy;

        videoData = new VideoData();
        //videoData.uiObject = Instantiate(uVideoPrefab);
        //videoData.uiObject.transform.SetParent(p.panel_VideoLayout.transform, false);
        videoData.uiObject = GetComponentInChildren<Tag_VideoImage>().gameObject;
        videoData.image = videoData.uiObject.GetComponentInChildren<RawImage>();
        //videoData.image.texture = texture_NoImage;

        if (isDummy)
            return;
        rtcManager.videoTable[ConnectionId.INVALID] = videoData;
        GetUIElement(nameof(button_Join), out button_Join);
        GetUIElement(nameof(button_Shutdown), out button_Shutdown);
        GetUIElement(nameof(inputField_RoomName), out inputField_RoomName);
        GetUIElement(nameof(toggle_Video), out toggle_Video);
        GetUIElement(nameof(toggle_Audio), out toggle_Audio);

        var randomCode = UnityEngine.Random.Range(0, 10000).ToString();
        SetName(randomCode);

        rtcManager.myPlayer = this;
        button_Join.onClick.RemoveAllListeners();
        button_Join.onClick.AddListener(OnClick_Button_Join);

        button_Shutdown.onClick.RemoveAllListeners();
        button_Shutdown.onClick.AddListener(OnClick_Button_Shutdown);

        toggle_Audio.onValueChanged.RemoveAllListeners();
        toggle_Audio.onValueChanged.AddListener(OnValueChange_Toggle_Audio);

        toggle_Video.onValueChanged.RemoveAllListeners();
        toggle_Video.onValueChanged.AddListener(OnValueChange_Toggle_Video);

        toggle_Audio.isOn = true;
        toggle_Video.isOn = true;
        //UnityCallFactory.RequestLogLevelStatic(UnityCallFactory.LogLevel.Info);
        UnityCallFactory.EnsureInit(OnCallFactoryReady, OnCallFactoryFailed);
        OnClick_Button_Join();
    }
    internal void SetName(string name)
    {
        Debug.Log($"Set Player Name. Prev={userName}, New={name}");
        userName = name;
        gameObject.name = $"GUEST_{userName}";
    }
    
    protected virtual void OnCallFactoryReady()
    {
        StartCoroutine(ExampleGlobals.RequestPermissions());
        mediaConfig.Video = toggle_Video.isOn;
        mediaConfig.Audio = toggle_Audio.isOn;
        mediaConfig.VideoDeviceName = UnityCallFactory.Instance.GetDefaultVideoDevice();
        netConfig.IceServers.Add(ExampleGlobals.DefaultIceServer);
        netConfig.SignalingUrl = ExampleGlobals.SignalingConference;
        netConfig.IsConference = true;
        this.inputField_RoomName.text = Application.productName + "_con";
    }
    protected virtual void OnCallFactoryFailed(string error)
    {
        string fullErrorMsg = typeof(CallApp).Name + " can't start. The " + typeof(UnityCallFactory).Name + " failed to initialize with following error: " + error;
        //Debug.LogError(fullErrorMsg);
    }
    private void Setup()
    {
        //Debug.Log("Creating ICall with " + NetConfig);
        EnsureLength();
        myCall = UnityCallFactory.Instance.Create(netConfig);
        if (myCall == null)
        {
            Debug.Log($"Create Call Error!! {userName}({name})");
            return;
        }

        myCall.CallEvent += rtcManager.CallEvent;
        myCall.Configure(mediaConfig);
        myCall.Listen(inputField_RoomName.text);
    }

    #region WebRTC
    public void Send(string msg)
    {
        if (String.IsNullOrEmpty(msg))
        {
            return;
        }

        myCall.Send(msg);
    }
    #endregion

    #region UI 
    private void EnsureLength()
    {
        var temp = inputField_RoomName.text;
        if (inputField_RoomName.text.Length > MAX_CODE_LENGTH)
        {
            inputField_RoomName.text = inputField_RoomName.text.Substring(0, MAX_CODE_LENGTH);
        }
        Debug.Log($"EnsureLength. Prev={temp}, New={inputField_RoomName.text}");
    }
    public void OnValueChange_Toggle_Audio(bool state)
    {
        Debug.Log($"OnValueChange_Toggle_Audio. {name}");
        mediaConfig.Audio = state;
    }
    public void OnValueChange_Toggle_Video(bool state)
    {
        Debug.Log($"OnValueChange_Toggle_Video. {name}");
        mediaConfig.Video = state;
    }
    public void OnClick_Button_Join()
    {
        Debug.Log($"OnClick_Join. {name}");
        Setup();
        button_Join.gameObject.SetActive(false);
    }
    public void OnClick_Button_Send()
    {
        Debug.Log($"{name}_SendRawData");
        Send(rtcManager.GetRawData(this));
    }
    public void OnClick_Button_Shutdown()
    {
        Debug.Log($"OnClick_ShutDown. {name}");
        rtcManager.CallEvent_Reset();
        button_Join.gameObject.SetActive(true);
    }
    #endregion
}
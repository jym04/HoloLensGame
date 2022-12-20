using Byn.Awrtc;
using Byn.Awrtc.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WIFramework.Util;
using Random = UnityEngine.Random;

public class WebRTCManager : MonoBehaviour
{
    #region LocalDefine
    enum DataCode
    {
        Raw,
        Input
    }
    enum ParsingOrder
    {
        DataCode,
        ID,
        Pos,
        Rot,
    }

    #endregion

    #region Variables
    public Player prefab_Player;
    public Player myPlayer;
    public GameObject uVideoPrefab;
    public Dictionary<ConnectionId, Player> playerTable = new Dictionary<ConnectionId, Player>();
    public Dictionary<ConnectionId, VideoData> videoTable = new Dictionary<ConnectionId, VideoData>();
    public Texture2D texture_NoImage;
    #endregion

    #region UnityMethods
    public void Awake()
    {
        //prefab_Player = Resources.Load<GameObject>("Player").GetComponent<Player>();
        myPlayer = SpawnPlayer();
    }
    #endregion

    #region CallEvent
    public void CallEvent(object sender, CallEventArgs e)
    {
        switch (e.Type)
        {
            case CallEventType.CallAccepted:
                var ca = e as CallAcceptedEventArgs;
                Debug.Log($"On New Call : {ca.ConnectionId}");
                CallEvent_NewCall(ca);
                break;

            case CallEventType.CallEnded:
                var ce = e as CallEndedEventArgs;
                Debug.Log($"On Call Ended : {ce.ConnectionId}");
                CallEvent_Ended(ce);
                break;

            case CallEventType.ListeningFailed:
                Debug.Log($"Listening Failed!!");
                CallEvent_Reset();
                break;

            case CallEventType.ConnectionFailed:
                ErrorEventArgs eea = e as ErrorEventArgs;
                Debug.LogError(eea.Info);
                CallEvent_Reset();
                break;

            case CallEventType.FrameUpdate:
                FrameUpdateEventArgs frameargs = e as FrameUpdateEventArgs;
                UpdateFrame(frameargs);
                break;

            case CallEventType.Message:
                MessageEventArgs mea = e as MessageEventArgs;
                Debug.Log($"Receive Message From={mea.ConnectionId}");
                CallMessageParsing(mea);
                break;

            case CallEventType.WaitForIncomingCall:
                WaitForIncomingCallEventArgs wic = e as WaitForIncomingCallEventArgs;
                Debug.Log($"Wait For Incomming Call Event... {wic.Address}");
                break;
        }
    }

    private void UpdateFrame(FrameUpdateEventArgs args)
    {
        if (videoTable.ContainsKey(args.ConnectionId))
        {
            VideoData videoData = videoTable[args.ConnectionId];
            //make sure not to overwrite / destroy our texture for missing image data
            if (videoData.image.texture == texture_NoImage)
                videoData.image.texture = null;
            bool mirror = args.IsRemote == false;
            //converts the frame data to a texture and sets it to the raw image
            UnityMediaHelper.UpdateRawImageTransform(videoData.image, args.Frame, mirror);
            videoData.texture = videoData.image.texture as Texture2D;
        }
    }
    void CleanupCall()
    {
        if (myPlayer.myCall == null)
            return;
        //Debug.Log("Destroying call!");
        myPlayer.myCall.Dispose();
        myPlayer.myCall = null;
        //Debug.Log("Call destroyed");
    }

    public void CallEvent_Reset()
    {
        foreach (var v in videoTable)
        {
            Destroy(v.Value.uiObject);
            if (v.Value.texture != null)
                Destroy(v.Value.texture);
        }
        videoTable.Clear();
        CleanupCall();
    }
    private void CallEvent_NewCall(CallAcceptedEventArgs args)
    {
        if (!playerTable.ContainsKey(args.ConnectionId))
        {
            var p = FindPlayer(args.ConnectionId);
        }

        myPlayer.Send(GetRawData(myPlayer));
    }

    private void CallEvent_Ended(CallEndedEventArgs args)
    {
        Debug.Log($"On Call End : {args.ConnectionId}");
        VideoData data;
        if (videoTable.TryGetValue(args.ConnectionId, out data))
        {
            if (data.texture != null)
                Destroy(data.texture);
            Destroy(data.uiObject);
            videoTable.Remove(args.ConnectionId);
        }

        OnUserLeft(args.ConnectionId);
    }

    private void OnUserLeft(ConnectionId id)
    {
        Debug.Log($"On User Left : {id}");
        Debug.Log($"Try Get Left User...");
        if (!playerTable.ContainsKey(id))
        {
            Debug.Log($"On User Left Into Wrong Connection({id}) Id.");
            return;
        }

        var target = playerTable[id];
        Debug.Log($"Find Left User. connectionId={id}, userName={target.userName}");
        Debug.Log($"Remove Left User Datas...");
        playerTable.Remove(id);
        Debug.Log($"Remove Left User From Player Table");
        videoTable.Remove(id);
        Debug.Log($"Remove Left User Video Frame");
        Destroy(target.gameObject);
        Debug.Log($"Remove Left User Objects");
    }

    #endregion

    #region DataParsing
    
    public string GetRawData(Player p)
    {
        var t = p.transform;
        return $"{DataCode.Raw}/{p.userName}/" +
            $"{t.position.x},{t.position.y},{t.position.z}/" +
            $"{t.rotation.x},{t.rotation.y},{t.rotation.z},{t.rotation.w}";
    }
    public Player SpawnPlayer(bool isDummy = false)
    {
        var player = Instantiate(prefab_Player);
        player.Initialize(isDummy);
        return player;
    }

    void CallMessageParsing(MessageEventArgs args)
    {
        Debug.Log($"Call Message Parsing... {args.Content}");
        var dataSplit = args.Content.Split('/');
        DataCode dataType = (DataCode)Enum.Parse(typeof(DataCode), dataSplit[0]);
        Debug.Log($"Call Message Type = {dataType}");
        switch (dataType)
        {
            case DataCode.Raw:
                ExecuteRawData(args, dataSplit);
                break;
            case DataCode.Input:
                ExecuteInputData(dataSplit);
                break;
        }
    }

    public enum CustomKeyCode
    {
        None,
        W,
        S,
        A,
        D
    }

    public CustomKeyCode CustomKeyCodeMatching(KeyCode keyCode)
    {
        if (keyCode == KeyCode.W)
            return CustomKeyCode.W;

        if (keyCode == KeyCode.A)
            return CustomKeyCode.A;

        if (keyCode == KeyCode.S)
            return CustomKeyCode.S;

        if (keyCode == KeyCode.D)
            return CustomKeyCode.D;

        return CustomKeyCode.None;
    }

    /// <summary>
    /// dataType/Id/CustomKeyCode
    /// </summary>
    /// <param name="dataSplit"></param>
    private void ExecuteInputData(string[] dataSplit)
    {
        Debug.Log($"Excute Input Data...");
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Parsing Rule 
    /// DataType/ID/x,y,z/x,y,z,w
    /// [0]/[1]/[2]/[3]
    /// enum/string/vector3/quarternion
    /// </summary>
    private void ExecuteRawData(MessageEventArgs args, string[] dataSplit)
    {
        Debug.Log($"Execute Raw Data...");

        var id = dataSplit[(int)ParsingOrder.ID];
        Debug.Log($"Parsing Id={id}");

        var targetPlayer = FindPlayer(args, id);
        Debug.Log($"Parsing Player={targetPlayer.userName}");

        var rawPos = dataSplit[(int)ParsingOrder.Pos].Split(',');
        Vector3 pos = new Vector3(float.Parse(rawPos[0]), float.Parse(rawPos[1]), float.Parse(rawPos[2]));
        Debug.Log($"Parsing Pos={pos}");

        var rawRot = dataSplit[(int)ParsingOrder.Rot].Split(',');
        Quaternion rot = new Quaternion(float.Parse(rawRot[0]), float.Parse(rawRot[1]), float.Parse(rawRot[2]), float.Parse(rawRot[3]));
        Debug.Log($"Parsing Rot={rot}");

        targetPlayer.transform.position = pos;
        targetPlayer.transform.rotation = rot;
    }

    #endregion

    #region Player
    public void MutePlayer(Player p, bool isMute)
    {
        p.myCall.SetMute(isMute);
    }

    public void MediaConfigChange_VideoOutput(Player p, bool isOn)
    {
        p.mediaConfig.Video = isOn;
    }

    public void MediaConfigChange_AudioOutput(Player p, bool isOn)
    {
        p.mediaConfig.Audio = isOn;
    }

    internal Player CreateDummyPlayer()
    {
        Debug.Log($"Create Dummy Player");
        var p = SpawnPlayer(true);
        return p;
    }
    public Player FindPlayer(ConnectionId id)
    {
        Debug.Log($"Find Player ConnectionId = {id}");
        Debug.Log($"Try Get ConnectionId({id})");
        if (playerTable.ContainsKey(id))
        {
            return playerTable[id];
        }

        Debug.Log($"Can`t Find ConnectionId({id})");
        var dummy = CreateDummyPlayer();
        dummy.Initialize(true);
        playerTable.Add(id, dummy);
        videoTable.Add(id, dummy.videoData);
        Debug.Log($"Add New DummyPlayer. connectionId={id}, playerName={name}");

        var result = playerTable[id];
        return result;
    }
    internal Player FindPlayer(MessageEventArgs args, string name)
    {
        var result = FindPlayer(args.ConnectionId);
        result.SetName(name);
        return result;
    }
    #endregion
}

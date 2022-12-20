using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

// Mqtt 데이터 가공
public class M2MqttClient : MonoBehaviour
{
    //public float doosanInputTime;
    //public float kukaInputTime;

    //void Update()
    //{
    //}

    //public void ArrangeData(string msg, string topic, string topicDoosan, string topicKuka)
    //{
    //    print("msg " + msg);
    //    int dateTimePos = msg.LastIndexOf("DataTime") + 19;

    //    DateTime edgeTime = Convert.ToDateTime(msg.Substring(dateTimePos, 24));
    //    DateTime currentTime = DateTime.Now;
    //    TimeSpan timeDiff = currentTime - edgeTime;
        
    //    string msgJson = "{\"data\":" + msg + "}";

    //    doosanInfoList.Clear();
    //    //yaskawaInfoList.Clear();
    //    kukaInfoList.Clear();

    //    var info = FromJson<RobotMoveInfo>(msgJson);
 
    //    if (topic.Equals(topicDoosan))
    //    {
    //        foreach (var data in info)
    //        {
    //            doosanInfoList.Add(data);
    //        }

    //        if((doosanInfoList[doosanRobot.joints.Length * 2 + 6].value / 10f) > 1f && 
    //          (doosanInfoList[doosanRobot.joints.Length * 2 + 6 + 1].value / 10f) > 1f && 
    //          (doosanInfoList[doosanRobot.joints.Length * 2 + 6 + 2].value / 10f) > 1f)
    //        {
    //            doosanInputTime = 0f;
    //            SystemManager.instance.doosanConditionText = "구동중";
    //            SystemManager.instance.doosanConditionTextColor = SystemManager.instance.on;
    //        }

    //        double tempPosXDiff = Math.Abs(timeDiff.Milliseconds * (doosanInfoList[doosanRobot.joints.Length * 2 + 6].value/10f));
    //        double posXDiff = Convert.ToDouble(timeDiff.Seconds) + tempPosXDiff/1000f;
    //        double tempPosYDiff = Math.Abs(timeDiff.Milliseconds * (doosanInfoList[doosanRobot.joints.Length * 2 + 6 + 1].value/10f));
    //        double posYDiff = Convert.ToDouble(timeDiff.Seconds) + tempPosYDiff/1000f;
    //        double tempPosZDiff = Math.Abs(timeDiff.Milliseconds * (doosanInfoList[doosanRobot.joints.Length * 2 + 6 + 2].value/10f));
    //        double posZDiff = Convert.ToDouble(timeDiff.Seconds) + tempPosZDiff/1000f;

    //        Logger.Append("Doosan", edgeTime.ToString(), currentTime.ToString(), timeDiff.ToString(), posXDiff.ToString(), posYDiff.ToString(), posZDiff.ToString());
    //        doosanRobot.Move(doosanInfoList);
    //        robotInfoUI.SetCurrentRobotRealtimeInfo(doosanInfoList, "Doosan");
    //        doosanRobot.GetComponent<RobotPopupUI>().SetJointInfoAndTCP(doosanRobot.angles, doosanInfoList);
    //    }
    //    //else if (topic.Equals(topicYaskawa))
    //    //{
    //    //    foreach (var data in info)
    //    //    {
    //    //        print("Yaskawa " + data.value);
    //    //        yaskawaInfoList.Add(data);
    //    //    }
    //    //    Logger.Append("Yaskawa", edgeTime.ToString(), currentTime.ToString(), timeDiff.ToString());
    //    //    yaskawaRobot.Move(yaskawaInfoList);
    //    //    robotInfoUI.SetCurrentRobotRealtimeInfo(yaskawaInfoList, "Yaskawa");
    //    //    //yaskawaRobot.GetComponent<RobotPopupUI>().SetJointAngleAndTorque(yaskawaRobot.angles, yaskawaInfoList);
    //    //}
    //    else if (topic.Equals(topicKuka))
    //    {
    //        foreach (var data in info)
    //        {
    //            kukaInfoList.Add(data);
    //        }

    //        if ((kukaInfoList[kukaRobot.joints.Length * 2 + 6].value / 10f) > 1f &&
    //           (kukaInfoList[kukaRobot.joints.Length * 2 + 6 + 1].value / 10f) > 1f &&
    //           (kukaInfoList[kukaRobot.joints.Length * 2 + 6 + 2].value / 10f) > 1f)
    //        {
    //            kukaInputTime = 0f;
    //            SystemManager.instance.kukaConditionText = "구종중";
    //            SystemManager.instance.kukaConditionTextColor = SystemManager.instance.on;

    //        }

    //        double tempPosXDiff = Math.Abs(timeDiff.Milliseconds * kukaInfoList[kukaRobot.joints.Length * 2 + 6].value);
    //        double posXDiff = Convert.ToDouble(timeDiff.Seconds) + tempPosXDiff/1000f;
    //        double tempPosYDiff = Math.Abs(timeDiff.Milliseconds * kukaInfoList[kukaRobot.joints.Length * 2 + 6 + 1].value);
    //        double posYDiff = Convert.ToDouble(timeDiff.Seconds) + tempPosYDiff/1000f;
    //        double tempPosZDiff = Math.Abs(timeDiff.Milliseconds * kukaInfoList[kukaRobot.joints.Length * 2 + 6 + 2].value);
    //        double posZDiff = Convert.ToDouble(timeDiff.Seconds) + tempPosZDiff/1000f;

    //        Logger.Append("Kuka", edgeTime.ToString(), currentTime.ToString(), timeDiff.ToString(), posXDiff.ToString(), posYDiff.ToString(), posZDiff.ToString());
    //        kukaRobot.Move(kukaInfoList);
    //        robotInfoUI.SetCurrentRobotRealtimeInfo(kukaInfoList, "Kuka");
    //        kukaRobot.GetComponent<RobotPopupUI>().SetJointInfoAndTCP(kukaRobot.angles, kukaInfoList);
    //    }
    //    else
    //    {
    //        Debug.LogError("topic error");
    //    }
    //}

    //public static T[] FromJson<T>(string json)
    //{
    //    Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
    //    return wrapper.data;
    //}

    //[System.Serializable]
    //private class Wrapper<T>
    //{
    //    public T[] data;
    //}
}

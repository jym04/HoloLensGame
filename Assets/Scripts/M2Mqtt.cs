using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

[System.Serializable]
public class OutputInfo
{
    public string tagId;
    public string name;
    public double value;
}

// Mqtt 연결
public class M2Mqtt : M2MqttTopicInfo
{
    public static M2Mqtt instance;
    //public M2MqttClient m2MqttClient;

    // MQTT setting
    protected MqttClient client;

    private string brokerAddress = "rpa-server.flexing.ai"; // rpa-server.flexing.ai
    private int brokerPort = 1883; // 원격 1883 로컬Ʈ 62658
    private string mqttUserName = null;
    private string mqttPassword = null;

    [Tooltip("Use encrypted connection")]
    public bool isEncrypted = false;
    [Tooltip("Connection timeout in milliseconds")]
    public int timeoutOnConnection = MqttSettings.MQTT_CONNECT_TIMEOUT;

    private List<MqttMsgPublishEventArgs> messageQueue1 = new List<MqttMsgPublishEventArgs>();
    private List<MqttMsgPublishEventArgs> messageQueue2 = new List<MqttMsgPublishEventArgs>();
    private List<MqttMsgPublishEventArgs> frontMessageQueue = null;
    private List<MqttMsgPublishEventArgs> backMessageQueue = null;

    private bool mqttClientConnectionClosed = false;
    private bool mqttClientConnected = false;

    //public Text[] log;
    //private int[] isConnect;

    // Siwha
    public Device_Status device_status;
    public Text cumulative_count;
    public Text daily_count_1;
    public Text daily_count_2;
    public Text main_air_pressure;
    public Text sholder_metal_control_temperature;
    public Text sholder_metal_cutting_temperature;
    public Text lubricant_supply_pressure;
    public Text process_oil_pressure;
    public Text sholder_metal_oil_supply_pressure_control;
    public Text sholder_metal_oil_supply_cutting_control;
    public Oil_Pressure_Level_Warning oil_pressure_level_warning;
    public Text main_motor_rpm;

    // 광주
    public Device_Status device_status_615;
    public Text cumulative_count_615;
    public Text daily_count_1_615;
    public Text daily_count_2_615;
    public Text main_air_pressure_615;
    public Text sholder_metal_control_temperature_615;
    public Text sholder_metal_cutting_temperature_615;
    public Text lubricant_supply_pressure_615;
    public Text process_oil_pressure_615;
    public Text sholder_metal_oil_supply_pressure_control_615;
    public Text sholder_metal_oil_supply_cutting_contro_l_615;
    public Oil_Pressure_Level_Warning oil_pressure_level_warning_615;
    public Text main_motor_rpm_615;
   

    public List<string> topicList;

    void Awake() {
        instance = this;
        
        frontMessageQueue = messageQueue1;
        backMessageQueue = messageQueue2;

        topicList = new List<string>() { 
        Siwha //514 
        // 광주 추가
        };

    }

    void Start()
    {
        //Thread thread = new Thread(Connect);
        //thread.Start();
        //isConnect = new int[20];
        Connect();
        //thread.Abort();
    }

    void Update()
    {
        ProcessMqttEvents();
    }

    protected virtual void ProcessMqttEvents()
    {
        SwapMqttMessageQueues();
        ProcessMqttMessageBackgroundQueue();
    }
    
    private void ProcessMqttMessageBackgroundQueue()
    {
        foreach (MqttMsgPublishEventArgs msg in backMessageQueue)
        {
            DecodeMessage(msg.Topic, msg.Message);
        }
        backMessageQueue.Clear();
    }

    private void DecodeMessage(string topic, byte[] message)
    {
        string msg = System.Text.Encoding.UTF8.GetString(message); 
        //Debug.Log(msg);
        string msgJson = "{\"data\":" + msg + "}";
        Debug.Log(msgJson);

        var info = FromJson<OutputInfo>(msgJson);
        int leng = info.Length;
        for (int i = 0; i < info.Length; ++i) 
        {
            Debug.Log($"{leng}::{info[i].name}/{info[i].tagId}/{info[i].value}");

            switch (info[i].name)
            {
                
                case "device_status":
                    if (info[i].value == 0)
                    {
                        device_status.GetComponent<Device_Status>().Value_0();
                    }
                    if (info[i].value == 1)
                    {
                        device_status.GetComponent<Device_Status>().Value_1();
                    }
                    if (info[i].value == 2)
                    {
                        device_status.GetComponent<Device_Status>().Value_2();
                    }
                    break;
                
                case "cumulative_count_1":
                    cumulative_count.text = info[i].value.ToString();
                    break;

                case "cumulative_count_2":
                    cumulative_count.text += info[i].value.ToString();
                    break;

                case "daily_count_1_A":
                    daily_count_1.text = info[i].value.ToString();
                    break;

                case "daily_count_1_B":
                    daily_count_1.text += info[i].value.ToString();
                    break;

                case "daily_count_2_A":
                    daily_count_2.text = info[i].value.ToString();
                    break;

                case "daily_count_2_B":
                    daily_count_2.text += info[i].value.ToString();
                    break;

                case "main_air_pressure":
                    main_air_pressure.text = info[i].value.ToString();
                    break;

                case "sholder_metal_control_temperature":
                    sholder_metal_control_temperature.text = info[i].value.ToString();
                    break;

                case "sholder_metal_cutting_temperature":
                    sholder_metal_cutting_temperature.text = info[i].value.ToString();
                    break;

                case "lubricant_supply_pressure":
                    lubricant_supply_pressure.text = info[i].value.ToString();
                    break;

                case "process_oil_pressure":
                    process_oil_pressure.text = info[i].value.ToString();
                    break;

                case "sholder_metal_oil_supply_pressure_control":
                    sholder_metal_oil_supply_pressure_control.text = info[i].value.ToString();
                    break;

                case "sholder_metal_oil_supply_cutting_control":
                    sholder_metal_oil_supply_cutting_control.text = info[i].value.ToString();
                    break;
                    
                case "oil_pressure_level_warning":
                    if (info[i].value == 0)
                    {
                        oil_pressure_level_warning.GetComponent<Oil_Pressure_Level_Warning>().Normal();
                    }
                    else if(info[i].value == 1)
                    {
                        oil_pressure_level_warning.GetComponent<Oil_Pressure_Level_Warning>().Abnormal();
                    }
                    break;
                    
                case "main_motor_rpm":
                    main_motor_rpm.text = info[i].value.ToString() + "rpm";
                    break;
            } 
        }

        // log에 String으로 값 표현
        //for (int i = 0; i < topicList.Count; i++)
        //{
        //    if(i == 8)
        //    {
        //        log[i].text = info[0].value.ToString();
        //    }
        //    else
        //    {
        //        if (i == 12)
        //        {
        //            log[i].text = info[4].value.ToString();
        //            isConnect[i] = Convert.ToInt32(info[5].value);

        //            if (isConnect[i] == 0)
        //            {
        //                red[i].ActiveTrue();
        //                green[i].ActiveFalse();
        //            }
        //            else
        //            {
        //                red[i].ActiveFalse();
        //                green[i].ActiveTrue();
        //            }
        //        }
        //        if (i == 16)
        //        {
        //            log[i].text = info[2].value.ToString();
        //            isConnect[i] = Convert.ToInt32(info[3].value);
        //        }
        //        else if (i == 17)
        //        {
        //            log[i].text = info[16].value.ToString();
        //            isConnect[i] = Convert.ToInt32(info[29].value);
        //        }
        //        else
        //        {
        //            log[i].text = info[0].value.ToString();
        //            isConnect[i] = Convert.ToInt32(info[1].value);
                    
        //        }

        //        print("log " + i + " " + log[i].text);
        //        print("isConnect " + i + " " + isConnect[i]);

        //        if (isConnect[i] == 0)
        //        {
        //            red[i].ActiveTrue();
        //            green[i].ActiveFalse();
        //        }
        //        else
        //        {
        //            red[i].ActiveFalse();
        //            green[i].ActiveTrue();
        //        }
        //    }
        //}

    }

    private void SwapMqttMessageQueues()
    {
        frontMessageQueue = frontMessageQueue == messageQueue1 ? messageQueue2 : messageQueue1;
        backMessageQueue = backMessageQueue == messageQueue1 ? messageQueue2 : messageQueue1;
    }

    public void Connect()
    {
        if (client == null || !client.IsConnected)
        {
            StartCoroutine(DoConnect());
        }
    }

    private IEnumerator DoConnect()
    {
        if (client == null)
        {
            try
            {
                client = new MqttClient(brokerAddress, brokerPort, isEncrypted, null, null, isEncrypted ? MqttSslProtocols.SSLv3 : MqttSslProtocols.None);
            }
            catch (Exception e)
            {
                client = null;
                OnConnectionFailed(e.Message);
                StartCoroutine(DoConnect());
                yield break;
            }
        }
        else if (client.IsConnected)
        {
            yield break;
        }

        client.Settings.TimeoutOnConnection = timeoutOnConnection;

        string clientId = Guid.NewGuid().ToString();

        try
        {
            client.Connect(clientId, mqttUserName, mqttPassword);
        }
        catch (Exception e)
        {
            client = null;
            OnConnectionFailed(e.Message);
            StartCoroutine(DoConnect());
            yield break;
        }

        if (client.IsConnected)
        {
            client.ConnectionClosed += OnMqttConnectionClosed;
            client.MqttMsgPublishReceived += OnMqttMessageReceived;

            mqttClientConnected = true;
            SubscribeTopics();
            StartCoroutine(OnConnectionSuccess());
        }
    }

    private void OnConnectionFailed(string errorMessage)
    {
        Debug.LogWarning("Connection failed. " + errorMessage + " " + brokerAddress + " " + brokerPort);
    }

    private IEnumerator OnConnectionSuccess()
    {
        //log.text = "Connected to " + brokerAddress + " " + brokerPort.ToString();
        yield return new WaitForSeconds(1.5f);
       // log.text = "";
    }

    private void OnMqttConnectionClosed(object sender, EventArgs e)
    {
        Debug.LogWarning("CONNECTION LOST!");
        StartCoroutine(DoDisconnect());
        mqttClientConnectionClosed = mqttClientConnected;
        mqttClientConnected = false;
    }

    public void OnMqttConnectionNoAuleClosed()
    {
        Debug.LogWarning("CONNECTION LOST!");
        StartCoroutine(DoDisconnect());
        mqttClientConnectionClosed = mqttClientConnected;
        mqttClientConnected = false;
    }

    private void OnMqttMessageReceived(object sender, MqttMsgPublishEventArgs msg)
    {
        frontMessageQueue.Add(msg);
    }

    private void SubscribeTopics()
    {
        for (int i = 0; i < topicList.Count; i++)
        {
            client.Subscribe(new string[] { topicList[i] }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }
    }

    private void UnsubscribeTopics()
    {
        for (int i = 0; i < topicList.Count; i++)
        {
            client.Unsubscribe(new string[] { topicList[i] });
        }
    }

    private IEnumerator DoDisconnect()
    {
        yield return new WaitForEndOfFrame();
        CloseConnection();
        Debug.Log("Disconnected.");
    }

    public void CloseConnection()
    {
        mqttClientConnected = false;
        if (client != null)
        {
            if (client.IsConnected)
            {
                UnsubscribeTopics();
                client.Disconnect();
            }
            mqttClientConnectionClosed = false;
            client.MqttMsgPublishReceived -= OnMqttMessageReceived;
            client.ConnectionClosed -= OnMqttConnectionClosed;
            client = null;
            StartCoroutine(DoConnect());
        }
    }

    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.data;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] data;
    }
}

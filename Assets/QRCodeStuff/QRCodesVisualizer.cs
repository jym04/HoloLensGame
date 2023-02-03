using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.QR;
using TMPro;

namespace QRTracking
{
    public class QRCodesVisualizer : MonoBehaviour
    {
        public GameObject qrCodePrefab;

        public GameObject mapScanScreen;
        public GameObject selectionScreen;
        public TextMeshPro scanningText;

        public System.Collections.Generic.SortedDictionary<string, GameObject> qrCodesObjectsList;
        private bool clearExisting = false;
        public GameObject[] mapObject;

        struct ActionData
        {
            public enum Type
            {
                Added,
                Updated,
                Removed
            };

            public Type type;
            public Microsoft.MixedReality.QR.QRCode qrCode;

            public ActionData(Type type, Microsoft.MixedReality.QR.QRCode qRCode) : this()
            {
                this.type = type;
                qrCode = qRCode;
            }
        }

        private System.Collections.Generic.Queue<ActionData> pendingActions = new Queue<ActionData>();

        void Awake()
        {
        }

        // Use this for initialization
        void Start()
        {
            scanningText.text = "Start Scan 버튼을 눌러주세요";
            qrCodesObjectsList = new SortedDictionary<string, GameObject>();

            // listen to any event changes on QRCOdeManager
            QRCodesManager.Instance.QRCodesTrackingStateChanged += Instance_QRCodesTrackingStateChanged;
            QRCodesManager.Instance.QRCodeAdded += Instance_QRCodeAdded;
            QRCodesManager.Instance.QRCodeUpdated += Instance_QRCodeUpdated;
            QRCodesManager.Instance.QRCodeRemoved += Instance_QRCodeRemoved;
            if (qrCodePrefab == null)
            {
                throw new System.Exception("Prefab not assigned");
            }
        }

        // call this whenever the state has changed - line 120 of QRCodesManager - this method is invoked when we start QR Tracking from QRCodesManager
        private void Instance_QRCodesTrackingStateChanged(object sender, bool status)
        {
            if (!status)
            {
                clearExisting = true;
            }
        }

        // listen to QRCodesManager changes on QRCodeVisualizer
        private void Instance_QRCodeAdded(object sender, QRCodeEventArgs<Microsoft.MixedReality.QR.QRCode> e)
        {
            Debug.Log("QRCodesVisualizer Instance_QRCodeAdded");

            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Added, e.Data));
            }
        }

        private void Instance_QRCodeUpdated(object sender, QRCodeEventArgs<Microsoft.MixedReality.QR.QRCode> e)
        {
            Debug.Log("QRCodesVisualizer Instance_QRCodeUpdated");

            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Updated,
                    e.Data)); // Enqueue adds an object to the end of the Queue
            }
        }

        private void Instance_QRCodeRemoved(object sender, QRCodeEventArgs<Microsoft.MixedReality.QR.QRCode> e)
        {
            Debug.Log("QRCodesVisualizer Instance_QRCodeRemoved");

            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Removed, e.Data));
            }
        }

        private void HandleEvents()
        {
            lock (pendingActions)
            {
                while (pendingActions.Count > 0)
                {
                    var action = pendingActions.Dequeue(); // removes an element from the queue FIFO approach
                    if (action.type == ActionData.Type.Added)
                    {
                        GameObject qrCodeObject = Instantiate(qrCodePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        qrCodeObject.GetComponent<SpatialGraphNodeTracker>().Id = action.qrCode.SpatialGraphNodeId;
                        qrCodeObject.GetComponent<QRCode>().qrCode = action.qrCode;
                        qrCodesObjectsList.Add(action.qrCode.Data, qrCodeObject); //QRcode added
                        if (qrCodesObjectsList.ContainsKey(action.qrCode.Data))
                        {
                            Destroy(qrCodesObjectsList[action.qrCode.Data]);
                            qrCodesObjectsList.Remove(action.qrCode.Data);
                        }
                    }
                    else if (action.type == ActionData.Type.Updated)
                    {
                        if (!qrCodesObjectsList.ContainsKey(action.qrCode.Data))
                        {
                            GameObject qrCodeObject =
                                Instantiate(qrCodePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                            qrCodeObject.GetComponent<SpatialGraphNodeTracker>().Id = action.qrCode.SpatialGraphNodeId;
                            qrCodeObject.GetComponent<QRCode>().qrCode = action.qrCode;
                            //if same key is already there, update its position
                            if (qrCodesObjectsList.ContainsKey(action.qrCode.Data))
                            {
                                Destroy(qrCodesObjectsList[action.qrCode.Data]);
                                qrCodesObjectsList.Remove(action.qrCode.Data);
                            }
                            qrCodesObjectsList.Add(action.qrCode.Data, qrCodeObject);
                        }
                    }
                    else if (action.type == ActionData.Type.Removed)
                    {
                        if (qrCodesObjectsList.ContainsKey(action.qrCode.Data))
                        {
                            Destroy(qrCodesObjectsList[action.qrCode.Data]);
                            qrCodesObjectsList.Remove(action.qrCode.Data);
                        }
                    }
                }
            }

            if (clearExisting)
            {
                clearExisting = false;
                foreach (var obj in qrCodesObjectsList)
                {
                    Destroy(obj.Value);
                }

                qrCodesObjectsList.Clear();
            }
        }

        // Update is called once per frame
        void Update()
        {
            HandleEvents();
            ScanQRCode();
        }
        private void ScanQRCode()
        {
            if (qrCodesObjectsList.Count > 0)
            {
                scanningText.text = "스캔이 완료되었습니다, Stop Scan 을 눌러주세요.";
            }
        }

        public void StartScan()
        {
            QRCodesManager.Instance.StartQRTracking();
            scanningText.text = "QRCode 를 바라봐 주세요.";
        }

        public void StopScan()
        {
            foreach(var qrcode in qrCodesObjectsList)
            {
                mapObject[0].transform.localPosition = qrcode.Value.gameObject.transform.localPosition;
                mapObject[1].transform.localPosition = qrcode.Value.gameObject.transform.localPosition;

                mapObject[0].transform.rotation = Quaternion.Euler(0, qrcode.Value.gameObject.transform.localRotation.eulerAngles.y + qrcode.Value.gameObject.transform.localRotation.eulerAngles.z, 0);
                mapObject[1].transform.rotation = Quaternion.Euler(0, qrcode.Value.gameObject.transform.localRotation.eulerAngles.y + qrcode.Value.gameObject.transform.localRotation.eulerAngles.z, 0);
            }
            QRCodesManager.Instance.StopQRTracking();
            scanningText.text = "Map Scanning Complete";

            selectionScreen.SetActive(true);
            mapScanScreen.SetActive(false);
        }

    }



}
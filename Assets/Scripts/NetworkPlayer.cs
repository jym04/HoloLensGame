using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class NetworkPlayer : MonoBehaviour
{
    private Player player;
    private Transform xrRig;
    public Transform body;
    public Transform leftHand, rightHand;
    public Transform faceUI;
    public TMP_Text nameText;

    void Start()
    {
        player = GetComponent<Player>();

        xrRig = GameObject.FindWithTag("Player").transform;

        nameText.text = gameObject.name;
        //for (int i = 1; i <= 6; i++)
        //{
        //    faceUI.transform.GetChild(i).gameObject.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isDummy)
        {
            body.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(false);
            rightHand.gameObject.SetActive(false);
            MapPosition(body, XRNode.Head);
            MapPosition(leftHand, XRNode.LeftHand);
            MapPosition(rightHand, XRNode.RightHand);
            player.OnClick_Button_Send();
        }  
    }

    void MapPosition(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        target.localPosition = position;
        target.localRotation = rotation;
        target.parent.position = xrRig.transform.position;
        target.parent.rotation = xrRig.transform.rotation;
    }
}

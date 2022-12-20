using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class AvatarMove : MonoBehaviour
{
    private float moveSpeed = 9f;
    private float rotaveSpeed = 8f;
    private Vector3 moveDirection;
    private Vector3 angleDirection;
    public Camera cam;
    public GameObject ray, leftHand, rightHand;
    Player player;
    bool isClick;

    private void Awake()
    {
        //track = cam.GetComponent<TrackedDevice>();
        
        player = GetComponent<Player>();
        //ray = transform.GetChild(0).GetChild(0).gameObject;
        //leftHand = transform.GetChild(0).GetChild(1).gameObject;
        //rightHand = transform.GetChild(0).GetChild(2).gameObject;
    }

    private void Start()
    {
        cam.gameObject.transform.position = Vector3.zero;
        cam.gameObject.transform.eulerAngles = Vector3.zero;
        angleDirection = new Vector3(0, 90, 0);
    }

    private void Update()
    {
        //if (Keyboard.current.wasPressedThisFrame)
        //{
        //    isMove = !isMove;
        //}
        if (InputDevices.GetDeviceAtXRNode(XRNode.Head).isValid && !ray.activeSelf)
        {
            ray.SetActive(true);
            leftHand.SetActive(true);
            rightHand.SetActive(true);
        }
        else if (!InputDevices.GetDeviceAtXRNode(XRNode.Head).isValid && ray.activeSelf)
        {
            ray.SetActive(false);
            leftHand.SetActive(false);
            rightHand.SetActive(false);
        }


        if (!InputDevices.GetDeviceAtXRNode(XRNode.Head).isValid)
        {
            transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);
            if (Mouse.current.rightButton.ReadValue() == 1)
            {
                //cam.transform.position = Vector3.zero;
                //cam.transform.eulerAngles = Vector3.zero;
                MouseRotation();
                KeyboardMove();
            }
        }
      


        //player.OnClick_Button_Send();
    }

    // 마우스 회전
    void MouseRotation()
    {
        float xRotateSize = Mouse.current.delta.x.ReadValue();
        float yRotateSize = Mouse.current.delta.y.ReadValue();

        angleDirection += new Vector3(-yRotateSize, xRotateSize, 0) * rotaveSpeed * Time.deltaTime;

        //xRotate = Mathf.Clamp(xRotate + xRotateSize, -80, 30);
        transform.eulerAngles = angleDirection;
    }

    // 키보드 이동
    void KeyboardMove()
    {
        transform.position = ClampPosition();
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    // 이동 위치 제한
    Vector3 ClampPosition()
    {
        return new Vector3(
        Mathf.Clamp(transform.position.x, -25f, 29f),
        Mathf.Clamp(transform.position.y, -0.5f, 1f),
        Mathf.Clamp(transform.position.z, -16f, 22f)
        );
    }

    // 키보드 인풋 감지
    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        if (input != null)
        {
            moveDirection = new Vector3(input.x, 0f, input.y);
        }
        else
        {
            moveDirection = Vector3.zero;
        }
    }
}

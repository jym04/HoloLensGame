using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWebRTC : MonoBehaviour
{
    public Player player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.OnClick_Button_Send();
    }
}

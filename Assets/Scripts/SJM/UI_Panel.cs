using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UI_Panel : MonoBehaviour
{
    private bool panelState;
    //public GameObject panel;
    public Collider player;
    //public List<Collider> playerList = new List<Collider>();
    private Transform playerPosition;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Collider>();
        panelState = false;
        //panel.SetActive(false);
    }

    private void Update()
    {
        playerPosition = player.transform;
        transform.LookAt(playerPosition);
    }

    /*
    public void PanelActive()
    {
        if(panelState == false)
        {
            panel.SetActive(true);
            panelState = true;
        }
        else if(panelState == true)
        {
            panel.SetActive(false);
            panelState = false;
        }
    }
    */
}

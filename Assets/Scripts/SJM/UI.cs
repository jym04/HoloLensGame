using UnityEngine;

public class UI : MonoBehaviour
{
    public bool panelState;
    public GameObject panel;
    public Transform playerPosition;
    
    public int i;

    void Start()
    {
        panelState = false;
        panel.SetActive(false);
    }
    
    public void Active()
    {
        panel.SetActive(true);
        panelState = true;  
    }

    public void Destroy()
    {
        panel.SetActive(false);
        panelState = false;
    }

    /*
    public void LookPlayer(Transform player)
    {
        transform.LookAt(player);
    }
    */

    /*
    private void Update()
    {
        if (panelState == true)
        {
            transform.LookAt(playerPosition);
        } 
    }
    */
}

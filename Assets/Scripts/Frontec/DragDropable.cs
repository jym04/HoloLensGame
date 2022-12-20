using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DragDropable : MonoBehaviour
{


    Vector3 offset;
    public string destinationTag = "DropArea";
    bool Dropped = false;

    private void Start()
    {
     
    }
    public void OnSellected()
    {
       // gameObject.SetActive(true);
        transform.position = MouseWorldPosition();
    }

    public void OnMouseDown()
    {
        if (Dropped) return;
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
    }
 
    void OnMouseDrag()
    {
        if (Dropped) return;
        transform.position = MouseWorldPosition() + offset;
    }
 
    void OnMouseUp()
    {
        if (Dropped) return;
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;
        if(Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if(hitInfo.transform.tag == destinationTag)
            {
                transform.position = hitInfo.transform.position;
                Dropped = true;
            }
        }
        transform.GetComponent<Collider>().enabled = true;
    }
 
    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void Update()
    {

 
        
    }
}
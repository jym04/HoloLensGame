using PCS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    [SerializeField] LayerMask BoxLayer;
    [SerializeField] LayerMask Conveyor;

    public GameObject woodBox;
    private RaycastHit hit;
    private bool grabbed;

    private void Start()
    {
        grabbed = false;
        woodBox.SetActive(false);
    }
    private void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.right * -1, Color.cyan);
        if (Physics.Raycast(this.transform.position, this.transform.right * -1, 3, Conveyor ))
        {
            if (grabbed == false) return;
          
            woodBox.SetActive(false);
            // 우드박스 생성 해서 컨테이너로 보내기 
            GameObject bs = GameObject.Find("BoxSpawner");
            print(transform.root.name);
            switch (transform.root.name)
            {
                case "Robot":
                    print("Robot : Collision With Conveyor");
                    bs.GetComponent<BoxSpawner>().Spawn(0);
                    break;
                case "Robot2":
                    print("Robot2 :  Collision With Conveyor");
                    bs.GetComponent<BoxSpawner>().Spawn(1);
                    break;
                case "Robot3":
                    print("Robot3 :  Collision With Conveyor");
                    bs.GetComponent<BoxSpawner>().Spawn(2);
                    break;
                default:
                    break;
            }
            //Spawn Point를 정해서 스폰가능 
       
            grabbed = false;
            return;
        }


       

        if (Physics.Raycast(this.transform.position, this.transform.right * -1, 3, BoxLayer))
        {
            if (grabbed == true) return;
            print("Collision With Box");

            switch (transform.root.name)
            {
                case "Robot":
                    print("Robot:  Collision With Conveyor");
                    break;
                case "Robot2":
                    print("Robot2 :  Collision With Conveyor");

                    break;
                case "Robot3":
                    print("Robot3 : Collision With Conveyor");

                    break;
                default:
                    break;
            }
            woodBox.SetActive(true);
            grabbed = true;
        }
   
    }




}

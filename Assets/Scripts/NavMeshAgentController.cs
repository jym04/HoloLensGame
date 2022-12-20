using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position != target.transform.position) 
        {
            navMeshAgent.SetDestination(target.transform.position);
        }
    }
}

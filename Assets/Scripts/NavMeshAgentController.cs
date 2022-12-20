using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent navMeshAgent;
    private LineRenderer lineRenderer;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineRenderer.endWidth = 0.1f;
        lineRenderer.material.color = Color.blue;
        lineRenderer.enabled = false;
    }

    public void MakePath()
    {
        lineRenderer.enabled = true;
        StartCoroutine(MakePathCoroutine());
    }

    void DrawRoute()
    {
        int length = navMeshAgent.path.corners.Length;

        lineRenderer.positionCount = length;
        for (int i = 1; i < length; i++)
            lineRenderer.SetPosition(i, navMeshAgent.path.corners[i]);
    }

    IEnumerator MakePathCoroutine()
    {
        navMeshAgent.SetDestination(target.transform.position);
        lineRenderer.SetPosition(0, this.transform.position);

        while (Vector3.Distance(this.transform.position, target.transform.position) > 0.1f)
        {
            lineRenderer.SetPosition(0, this.transform.position);

            DrawRoute();

            yield return null;
        }

        lineRenderer.enabled = false;
    }
}

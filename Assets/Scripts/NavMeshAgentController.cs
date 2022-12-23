using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class NavMeshAgentController : MonoBehaviour
{
    public Transform target;
    public Transform startPoint;
    public Transform endPoint;
    private NavMeshAgent navMeshAgent;
    private LineRenderer lineRenderer;
    private PlayerSpawn playerSpawn;
    private GameButtonManager gameButtonManager;

    public TMP_Text pathStatus;
    public TMP_Text moveDistance;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        startPoint = GameObject.FindWithTag("StartPoint").GetComponent<Transform>();
        endPoint = GameObject.FindWithTag("EndPoint").GetComponent<Transform>();
        playerSpawn = GameObject.FindWithTag("Spawn").GetComponent<PlayerSpawn>();
        gameButtonManager = GameObject.FindWithTag("GameManager").GetComponent<GameButtonManager>();
        target = endPoint;

        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineRenderer.endWidth = 0.01f;
        lineRenderer.material.color = Color.blue;
        lineRenderer.enabled = false;
    }
    private void Update()
    {
        MakePath();
        ChangeTarget();
        UpdateUI();
    }

    public void MakePath()
    {
        lineRenderer.enabled = true;
        StopCoroutine(MakePathCoroutine());
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

        while (navMeshAgent.remainingDistance > 0.00006f)
        {
            lineRenderer.SetPosition(0, this.transform.position);

            DrawRoute();

            yield return null;
        }

        lineRenderer.enabled = false;

    }
    private void ChangeTarget()
    {
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    target = target == endPoint ? startPoint : endPoint;
                }
            }
        }
    }
    private void UpdateUI()
    {
        pathStatus.text = navMeshAgent.hasPath ? "Path Status : Complete" : "Path Status : None";
        moveDistance.text = navMeshAgent.hasPath ? "Move Distance : " + navMeshAgent.remainingDistance.ToString("F2") : "Move Distance : None";
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerSpawn.Spawn();

            Destroy(gameObject);
        }
    }
}

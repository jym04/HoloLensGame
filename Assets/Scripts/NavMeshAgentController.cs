using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public enum PlayerStatus
{
    Start,
    Move,
    Rotate
}
public class NavMeshAgentController : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public Transform target;
    public GameObject[] Point;
    private NavMeshAgent navMeshAgent;
    private LineRenderer lineRenderer;
    private float extraRotationSpeed = 10f;

    private PlayerSpawn playerSpawn;
    private GameButtonManager gameButtonManager;

    public TMP_Text pathStatus;
    public TMP_Text movedDistance;
    public TMP_Text workingTime;
    private float workTime;
    private bool isRotate;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        Point = GameObject.FindGameObjectsWithTag("Point");
        playerSpawn = GameObject.FindWithTag("Spawn").GetComponent<PlayerSpawn>();
        gameButtonManager = GameObject.FindWithTag("GameManager").GetComponent<GameButtonManager>();
        target = Point[1].transform;

        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineRenderer.endWidth = 0.005f;
        lineRenderer.enabled = false;
    }
    private void Update()
    {
        switch (playerStatus)
        {
            case PlayerStatus.Start: UpdateRotate(); break;
            case PlayerStatus.Move: MakePath(); ChangeTarget(); break;
            case PlayerStatus.Rotate: UpdateRotate(); break;
        }
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

        while (navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
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
                    if (gameButtonManager.gameType == GameType.DodgerGame)
                    {
                        if (target == Point[0].transform)
                        {
                            target = Point[1].transform;
                        }
                        else if (target == Point[1].transform)
                        {
                            target = Point[2].transform;
                        }
                        else if (target == Point[2].transform)
                        {
                            target = Point[3].transform;
                        }
                        else if (target == Point[3].transform)
                        {
                            target = Point[4].transform;
                        }
                        else if (target == Point[4].transform)
                        {
                            target = Point[5].transform;
                        }
                        else if (target == Point[5].transform)
                        {
                            target = Point[0].transform;
                        }
                        playerStatus = PlayerStatus.Rotate;
                    }
                    else if (gameButtonManager.gameType == GameType.MazeRunnerGame)
                    {
                        target = target == Point[1].transform ? Point[0].transform : Point[1].transform;
                        playerStatus = PlayerStatus.Rotate;
                    }
                }
            }
        }
    }
    private void UpdateUI()
    {
        pathStatus.text = navMeshAgent.hasPath ? "Path Status : Complete" : "Path Status : None";
        movedDistance.text = navMeshAgent.hasPath ? "Move Distance : " + navMeshAgent.remainingDistance.ToString("F2") : "Move Distance : 0.00";
        workTime = navMeshAgent.hasPath ? workTime += Time.deltaTime : 0f;
        workingTime.text = "Working Time : " + workTime.ToString("F2");
    }
    private void UpdateRotate()
    {
        Vector3 lookrotation = target.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(lookrotation), extraRotationSpeed * Time.deltaTime);

        float angle = Quaternion.Angle(transform.rotation,Quaternion.LookRotation(lookrotation));
        if (angle <= 2)
        {
            playerStatus = PlayerStatus.Move;
        }
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

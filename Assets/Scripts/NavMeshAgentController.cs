using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public enum PlayerStatus
{
    Start,
    Move,
    Stop
}
public class NavMeshAgentController : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public Transform target;
    public GameObject points;
    private NavMeshAgent navMeshAgent;
    private LineRenderer lineRenderer;
    public GameObject movementObjectCollection;
    public TMP_Text pointCount;
    private float extraRotationSpeed = 10f;

    private PlayerSpawn playerSpawn;
    private GameButtonManager gameButtonManager;
    private MovementObjectManager movementObjectManager;

    public TMP_Text pathStatus;
    public TMP_Text movedDistance;
    public TMP_Text workingTime;
    private float workTime;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        playerSpawn = GameObject.FindWithTag("Spawn").GetComponent<PlayerSpawn>();
        gameButtonManager = GameObject.FindWithTag("GameManager").GetComponent<GameButtonManager>();
        movementObjectManager = GameObject.FindWithTag("SpawnObject").GetComponent<MovementObjectManager>();
        pointCount = GameObject.FindWithTag("Debug").GetComponentInChildren<TMP_Text>();
        points = GameObject.FindWithTag("Point");
        movementObjectManager.SpawnObject();
        target = points.transform.GetChild(1);

        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.startWidth = lineRenderer.endWidth = 0.003f;
        lineRenderer.enabled = false;
    }
    private void FixedUpdate()
    {
        switch (playerStatus)
        {
            case PlayerStatus.Start: playerStatus = PlayerStatus.Stop; break;
            case PlayerStatus.Move: MakePath(); ChangeTarget(); break;
            case PlayerStatus.Stop: UpdateRotate(); break;
        }
        pointCount.text = target.ToString();
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

        while (navMeshAgent.hasPath)
        {
            lineRenderer.SetPosition(0, this.transform.position);

            Vector3 lookrotation = navMeshAgent.steeringTarget - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), Time.deltaTime);

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
                        if (target == points.transform.GetChild(0).transform)
                        {
                            target = points.transform.GetChild(1).transform;
                            movementObjectManager.SpawnObject();
                        }
                        else if (target == points.transform.GetChild(1).transform)
                        {
                            target = points.transform.GetChild(2).transform;
                        }
                        else if (target == points.transform.GetChild(2).transform)
                        {
                            target = points.transform.GetChild(3).transform;
                        }
                        else if (target == points.transform.GetChild(3).transform)
                        {
                            target = points.transform.GetChild(4).transform;
                        }
                        else if (target == points.transform.GetChild(4).transform)
                        {
                            target = points.transform.GetChild(5).transform;
                            Destroy(movementObjectCollection.transform.GetChild(0).gameObject);
                            movementObjectManager.arriveCount += 1;
                        }
                        else if (target == points.transform.GetChild(5).transform)
                        {
                            target = points.transform.GetChild(0).transform;
                        }
                        playerStatus = PlayerStatus.Stop;
                    }
                    else if (gameButtonManager.gameType == GameType.MazeRunnerGame)
                    {
                        if (target ==points.transform.GetChild(0).transform)
                        {
                            target = points.transform.GetChild(1).transform;
                        }
                        else if (target == points.transform.GetChild(1).transform)
                        {
                            target = points.transform.GetChild(0).transform;
                            Destroy(movementObjectCollection.transform.GetChild(0).gameObject);
                            movementObjectManager.arriveCount += 1;
                            movementObjectManager.SpawnObject();
                        }
                        playerStatus = PlayerStatus.Stop;
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
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), extraRotationSpeed * Time.deltaTime);

        StopCoroutine(RotateDelay());
        StartCoroutine(RotateDelay());
    }
    private IEnumerator RotateDelay()
    {
        yield return new WaitForSeconds(0.3f);

        playerStatus = PlayerStatus.Move;
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

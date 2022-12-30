using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public enum PlayerStatus
{
    Start,
    Move,
    Stop,
    Rotate
}
public class NavMeshAgentController : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public Transform target;
    public GameObject[] Point;
    private NavMeshAgent navMeshAgent;
    private LineRenderer lineRenderer;
    public GameObject movementObjectCollection;
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
        Point = GameObject.FindGameObjectsWithTag("Point");
        playerSpawn = GameObject.FindWithTag("Spawn").GetComponent<PlayerSpawn>();
        gameButtonManager = GameObject.FindWithTag("GameManager").GetComponent<GameButtonManager>();
        movementObjectManager = GameObject.FindWithTag("SpawnObject").GetComponent<MovementObjectManager>();
        movementObjectManager.SpawnObject();
        target = Point[1].transform;

        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineRenderer.endWidth = 0.005f;
        lineRenderer.enabled = false;
    }
    private void Update()
    {
        switch (playerStatus)
        {
            case PlayerStatus.Start: playerStatus = PlayerStatus.Rotate; break;
            case PlayerStatus.Move: MakePath(); break;
            case PlayerStatus.Stop:ChangeTarget();break;
            case PlayerStatus.Rotate: UpdateRotate(); break;
        }
        UpdateUI();
    }

    public void MakePath()
    {
        lineRenderer.enabled = true;
        StopCoroutine(MakePathCoroutine());
        StartCoroutine(MakePathCoroutine());
        playerStatus = PlayerStatus.Stop;
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
                            movementObjectManager.SpawnObject();
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
                            Destroy(movementObjectCollection.transform.GetChild(0).gameObject);
                            movementObjectManager.arriveCount += 1;
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
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(lookrotation), extraRotationSpeed * Time.deltaTime);

        Quaternion test = Quaternion.LookRotation(lookrotation);
        Quaternion test1 = transform.rotation;

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

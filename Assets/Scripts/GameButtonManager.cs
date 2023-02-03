using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTracking;

public enum GameType
{
    None,
    ObstacleAvoidance,
    PathPlanning
}
public class GameButtonManager : MonoBehaviour
{
    public GameType gameType;

    public GameObject obstacleAvoidanceMenu;
    public GameObject pathPlanningMenu;
    public GameObject selectMenu;
    public GameObject debugMenu;

    public GameObject obstacleAvoidanceMap;
    public GameObject pathPlanningMap;

    public ObstacleObjectManager obstacleObjectManager;
    public PlayerSpawn playerSpawn;
    public MovementObjectManager movementObjectManager;

    public NavigationBaker[] navigationBaker;
    public StartPoint startPoint;
    
    public void ObstacleAvoidanceButtonOnClick()
    {
        obstacleAvoidanceMenu.SetActive(true);
        gameType = GameType.ObstacleAvoidance;

        selectMenu.SetActive(false);
        pathPlanningMenu.SetActive(false);

        obstacleAvoidanceMap.SetActive(true);
        navigationBaker[0].Baked();

        playerSpawn.SetSpawn();
        playerSpawn.StartSpawn();
    }
    public void PathPlanningButtonOnClick()
    {
        pathPlanningMenu.SetActive(true);
        gameType = GameType.PathPlanning;

        selectMenu.SetActive(false);
        obstacleAvoidanceMenu.SetActive(false);

        pathPlanningMap.SetActive(true);
        navigationBaker[1].Baked();

        playerSpawn.SetSpawn();
        startPoint.isStart = true;
        QRCodesManager.Instance.StartQRTracking();
    }
    public void SpawnButtonOnClick()
    {
        obstacleObjectManager.InstantiateObjects();
    }
    public void ResetButtonOnClick()
    {
        if (gameType == GameType.ObstacleAvoidance)
        {
            obstacleObjectManager.ResetObjects();
        }
        else if (gameType == GameType.PathPlanning)
        {
            QRCodesManager.Instance.StartQRTracking();
            obstacleObjectManager.DeleteObjects();
            movementObjectManager.DeleteObjects();

            playerSpawn.Delete();
            playerSpawn.StopSpawn();
            playerSpawn.SetSpawn();
            startPoint.isStart = true;
            debugMenu.SetActive(false);
        }
    }
    public void PlayButtonOnClick()
    {
        if (GameObject.FindWithTag("Player") == null)
        {
            obstacleObjectManager.InstantiateObjects();

            StartCoroutine(PlayDelay());
        }
    }
    private IEnumerator PlayDelay()
    {
        yield return new WaitForSeconds(1f);

        QRCodesManager.Instance.StopQRTracking();

        if (startPoint.isStart == true)
        {
            playerSpawn.StartSpawn();
        }
        else
        {
            debugMenu.SetActive(true);
            startPoint.DebugingText();
        }
    }

    public void HomeButtonOnClick()
    {
        QRCodesManager.Instance.StopQRTracking();

        selectMenu.SetActive(true);
        obstacleAvoidanceMap.SetActive(false);
        pathPlanningMap.SetActive(false);

        playerSpawn.Delete();
        playerSpawn.StopSpawn();

        obstacleObjectManager.DeleteObjects();
        movementObjectManager.DeleteObjects();

        obstacleAvoidanceMenu.SetActive(false);
        pathPlanningMenu.SetActive(false);
        debugMenu.SetActive(false);

        gameType = GameType.None;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTracking;

public enum GameType
{
    None,
    DodgerGame,
    MazeRunnerGame
}
public class GameButtonManager : MonoBehaviour
{
    public GameType gameType;

    public GameObject dodgerGameMenu;
    public GameObject mazeRunnerGameMenu;
    public GameObject selectMenu;

    public GameObject dodgerGameMap;
    public GameObject mazeRunnerGameMap;

    public ObstacleObjectManager obstacleObjectManager;
    public PlayerSpawn playerSpawn;
    public MovementObjectManager movementObjectManager;

    public NavigationBaker[] navigationBaker;
    
    public void DodgerGameButtonOnClick()
    {
        dodgerGameMenu.SetActive(true);
        gameType = GameType.DodgerGame;

        selectMenu.SetActive(false);
        mazeRunnerGameMenu.SetActive(false);

        dodgerGameMap.SetActive(true);
        navigationBaker[0].Baked();
        playerSpawn.player = new GameObject[5];
        playerSpawn.Spawn();
    }
    public void MazeRunnerGameButtonOnClick()
    {
        mazeRunnerGameMenu.SetActive(true);
        gameType = GameType.MazeRunnerGame;

        selectMenu.SetActive(false);
        dodgerGameMenu.SetActive(false);

        mazeRunnerGameMap.SetActive(true);
        navigationBaker[1].Baked();
        playerSpawn.player = new GameObject[1];
        QRCodesManager.Instance.StartQRTracking();
    }
    public void SpawnButtonOnClick()
    {
        obstacleObjectManager.InstantiateObjects();
    }
    public void ResetButtonOnClick()
    {
        if (gameType == GameType.DodgerGame)
        {
            obstacleObjectManager.ResetObjects();
        }
        else if (gameType == GameType.MazeRunnerGame)
        {
            QRCodesManager.Instance.StartQRTracking();
            obstacleObjectManager.DeleteObjects();
            playerSpawn.Delete();
        }
        
    }
    public void PlayButtonOnClick()
    {
        if (GameObject.FindWithTag("Player") == null)
        {
            obstacleObjectManager.InstantiateObjects();
            playerSpawn.Spawn();
            StopCoroutine(StopScanning());
            StartCoroutine(StopScanning());
        }
    }
    private IEnumerator StopScanning ()
    {
        yield return new WaitForSeconds(1f);

        QRCodesManager.Instance.StopQRTracking();
    }

    public void HomeButtonOnClick()
    {
        selectMenu.SetActive(true);
        dodgerGameMap.SetActive(false);
        mazeRunnerGameMap.SetActive(false);

        if (gameType == GameType.DodgerGame)
        {
            playerSpawn.Delete();
            playerSpawn.StopCoroutine(playerSpawn.SpawnPlayer());
            playerSpawn.player = new GameObject[0];
        }
        else if (gameType == GameType.MazeRunnerGame)
        {
            playerSpawn.player = new GameObject[0];
            playerSpawn.Delete();
            playerSpawn.StopCoroutine(playerSpawn.SpawnPlayer());
        }

        obstacleObjectManager.DeleteObjects();
        movementObjectManager.DeleteObjects();

        dodgerGameMenu.SetActive(false);
        
        mazeRunnerGameMenu.SetActive(false);

        gameType = GameType.None;

    }
}

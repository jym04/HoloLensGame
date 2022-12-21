using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject player;
    public GameObject startPoint;


    public void DodgerGameButtonOnClick()
    {
        dodgerGameMenu.SetActive(true);
        gameType = GameType.DodgerGame;

        selectMenu.SetActive(false);
        mazeRunnerGameMenu.SetActive(false);

        GameObject car = Instantiate(player, startPoint.transform.position, Quaternion.identity);
    }
    public void MazeRunnerGameButtonOnClick()
    {
        mazeRunnerGameMenu.SetActive(true);
        gameType = GameType.MazeRunnerGame;

        selectMenu.SetActive(false);
        dodgerGameMenu.SetActive(false);
    }
    public void SpawnButtonOnClick()
    {

    }
    public void PlayButtonOnClick()
    {

    }
    public void ResetButtonOnClick()
    {

    }
    public void HomeButtonOnClick()
    {
        selectMenu.SetActive(true);
        gameType = GameType.None;

        dodgerGameMenu.SetActive(false);
        mazeRunnerGameMenu.SetActive(false);
    }
}

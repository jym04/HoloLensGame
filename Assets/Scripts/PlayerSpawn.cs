using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;

    private GameObject startPoint;
    public GameObject dodgerStartPoint;
    public GameObject mazeRunnerStartPoint;

    private GameObject[] player;
    private int playerMaxCount;

    public GameButtonManager gameButtonManager;

    public IEnumerator spawnCorutine;

    public void SetSpawn()
    {
        playerMaxCount = gameButtonManager.gameType == GameType.DodgerGame ? 5 : 1;
        player = new GameObject[playerMaxCount];
        startPoint = gameButtonManager.gameType == GameType.DodgerGame ? dodgerStartPoint : mazeRunnerStartPoint;
    }
    public void StartSpawn()
    {
        spawnCorutine = SpawnPlayer();
        StartCoroutine(spawnCorutine);
    }
    public void StopSpawn()
    {
        if (spawnCorutine != null)
        {
            StopCoroutine(spawnCorutine);
            playerMaxCount = 0;
            player = new GameObject[0];
        }
    }
    public IEnumerator SpawnPlayer()
    {
        WaitForSeconds checkDelay = new WaitForSeconds(2f);
        yield return checkDelay;

        if (gameButtonManager.gameType != GameType.None)
        {
            for (int i = 0; i < playerMaxCount; i++)
            {
                if (player[i] == null)
                {
                    player[i] = Instantiate(playerPrefab, startPoint.transform.position, Quaternion.identity, transform);
                }
                WaitForSeconds spawnDelay = new WaitForSeconds(2.5f);
                yield return spawnDelay;
            }
        }
    }
    public void Delete()
    {
        Transform[] childList = transform.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }
    }
}

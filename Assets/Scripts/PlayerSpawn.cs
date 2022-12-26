using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject startPoint;

    private GameObject player;

    public GameButtonManager gameButtonManager;
    public void Spawn()
    {
        StopCoroutine(SpawnPlayer());
        StartCoroutine(SpawnPlayer());
    }
    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(2f);

        if (gameButtonManager.gameType != GameType.None)
        {
            player = Instantiate(playerPrefab, startPoint.transform.position, Quaternion.identity);
        }
    }
    public void Delete()
    {
        Destroy(player);
    }
}

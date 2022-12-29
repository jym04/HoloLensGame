using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject startPoint;

    public GameObject[] player = new GameObject[5];

    public int playerMaxCount;

    public GameButtonManager gameButtonManager;
    public void Spawn()
    {
        StopCoroutine(SpawnPlayer());
        StartCoroutine(SpawnPlayer());
    }
    public IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(2f);

        if (gameButtonManager.gameType != GameType.None)
        {
            playerMaxCount = gameButtonManager.gameType == GameType.DodgerGame ? 5 : 1;

            for (int index = 0; index < player.Length; index++)
            {
                if (player[index] == null)
                {
                    Debug.Log(index);
                    player[index] = Instantiate(playerPrefab, startPoint.transform.position, Quaternion.identity);
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
    public void Delete()
    {
        
        for(int i = 0; i < playerMaxCount; i++)
        {
            if (player[i] != null)
            {
                Destroy(player[i]);
            }
        }
        player = new GameObject[5];
    }
}

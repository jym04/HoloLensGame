using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject startPoint;

    public void Spawn()
    {
        StopCoroutine(SpawnPlayer());
        StartCoroutine(SpawnPlayer());
    }
    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(2f);

        GameObject car = Instantiate(player, startPoint.transform.position, Quaternion.identity);
    }
}

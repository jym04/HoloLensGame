using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);

            Destroy(explosion, 2f);

            Destroy(gameObject);
        }
        
    }
}

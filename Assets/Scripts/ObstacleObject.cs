using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    public GameObject explosionPrefab;

    public GameButtonManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameButtonManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);

            Destroy(explosion, 2f);

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Map"))
        {
            if (gameManager.gameType == GameType.ObstacleAvoidance)
            {
                Quaternion temp = transform.rotation;

                temp.x = 0f;
                temp.z = 0f;

                gameObject.transform.rotation = temp;
            }
        }
    }
}

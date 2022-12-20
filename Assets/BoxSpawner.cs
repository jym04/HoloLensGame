
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCS
{
	public class BoxSpawner : MonoBehaviour
	{
		public GameObject box;
		public float interval;
		public float scale;
		public Transform[] spawnPoints;


		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame


		public void Spawn(int spawnPoint)
		{
			print("Spawn executed at point :" + spawnPoint);
			GameObject obj = Instantiate(box);
			obj.transform.localPosition = spawnPoints[spawnPoint].transform.position;
			obj.transform.parent = transform;
			
			obj.AddComponent<Rigidbody>();
			obj.AddComponent<PCSDemoCube>();

		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Maneger : MonoBehaviour {

	public Transform SpwanPoint;
	public int AmountOfEnemys;
	public GameObject Enemies;

	// Use this for initialization
	void Start () {
		initSpawn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void initSpawn()
	{
		for (int i = 0; i < AmountOfEnemys; i++)
		{
			Vector3 temp = new Vector3(SpwanPoint.position.x, SpwanPoint.position.y, SpwanPoint.position.z + 1);
			int x = Random.Range(0, 250);
			int z = Random.Range(0, 250);
			Instantiate(Enemies, temp, Quaternion.identity);
		}
	}
}

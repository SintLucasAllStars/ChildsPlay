using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Maneger : MonoBehaviour {

	public int AmountOfEnemys;
	public GameObject Enemies;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void initSpawn()
	{
		for (int i = 0; i < AmountOfEnemys; i++)
		{
			int x = Random.Range(0, 250);
			int z = Random.Range(0, 250);
			Instantiate(Enemies,new Vector3(x,20,z),Quaternion.identity);
		}
	}
}

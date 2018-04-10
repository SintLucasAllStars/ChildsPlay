using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour {

	public static GameBehaviour gb;

	public float gravity = -9.8f;

	List<GameObject> enems;
	int amountEnemies = 1;
	GameObject tagger;
	GameObject player;
	void Start () {
		if (GameBehaviour.gb == null)
			GameBehaviour.gb = this;
		else
			Destroy (this.gameObject);
		DontDestroyOnLoad (this);


		player = Instantiate (Resources.Load<GameObject> ("Prefabs/Jimmy"), GetSpawningPosition(), Quaternion.identity);
		enems = new List<GameObject> (amountEnemies);
		for (int i = 0; i < amountEnemies; i++)
			enems.Add (Instantiate (Resources.Load<GameObject> ("Prefabs/ShadowJimmy"), GetSpawningPosition (), Quaternion.identity));

		if (Random.Range (0, 2) == 0)
			tagger = player;
		else
			tagger = enems [Random.Range (0, enems.Count)];
	}

	Vector3 GetSpawningPosition(){
		//Random position where you can spawn;
		return new Vector3(Random.Range(-2f,2f),2f,Random.Range(-2f,2f));
	}
}

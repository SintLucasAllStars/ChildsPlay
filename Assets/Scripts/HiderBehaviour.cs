using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HiderBehaviour : MonoBehaviour {

	public Vector3 startPosition;
	public Vector3 hidingPosition;

	private GameObject seeker;
	private GameManager gm;

	public float mapSize;

	void Start () {

		//seeker = GameObject.FindGameObjectWithTag ("Seeker");
		gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		startPosition = transform.position;
		hidingPosition = new Vector3 (Random.Range (-mapSize/2, mapSize/2), 1, Random.Range (-mapSize/2, mapSize/2));

	}

	void Update () {
		
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	public GameObject forkInStone;
	public Vector3[] forkPositions;
	// Use this for initialization
	void Start () {
		Instantiate (forkInStone, forkPositions [Random.Range (0, forkPositions.Length)], Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

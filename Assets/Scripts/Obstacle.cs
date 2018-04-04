using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
	RaycastHit Hit;
	bool groundDetector;
	private TerrainGenerator terrainGenerator;

	// Use this for initialization
	void Start () {
		groundDetector = false;
		terrainGenerator = GameObject.Find("World").GetComponent<TerrainGenerator>();
		float y =  terrainGenerator.ReturnHeight(transform.position.x, transform.position.z);
		transform.position = new Vector3(transform.position.x,y,transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	/*if(groundDetector == false )	{
/*
	Debug.DrawRay (transform.position, -Vector3.up * 0.5f);
		if (Input.GetMouseButton (0)); {
			Ray myRay = new Ray (transform.position,-Vector3.up);
			if (Physics.Raycast (myRay, out Hit, 0.5f)) {
				if (Hit.collider.tag == "Ground") {
					Debug.Log ("Hit Ground");
					groundDetector = true;
				

					
					
					}
				}	
			}
		}	
	}*/	
	}
}


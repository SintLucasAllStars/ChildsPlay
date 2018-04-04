using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
	RaycastHit Hit;
	bool groundDetector;

	// Use this for initialization
	void Start () {
		groundDetector = false;
	}
	
	// Update is called once per frame
	void Update () {
		//while (groundDetector == false){
		//	transform.Translate(0,1f,0);
			
		//}
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
}

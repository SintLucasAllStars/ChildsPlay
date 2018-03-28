using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeek : MonoBehaviour {
	public GameObject[] hidePlaces = new GameObject[4];
	GameObject buffer;

	// Use this for initialization
	void Start (){
		
	}
	
	// Update is called once per frame
	void Update (){
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				buffer = hit.collider.gameObject;
				CheckWin ();
				/*if (hit.transform.name == "Hide1") {
					buffer = hidePlaces [0];
					CheckWin ();
				}
				if (hit.transform.name == "Hide2") {
					buffer = hidePlaces [1];
					CheckWin ();
				}
				if (hit.transform.name == "Hide3") {
					buffer = hidePlaces [2];
					CheckWin ();
				}
				if (hit.transform.name == "Hide4") {
					buffer = hidePlaces [3];
					CheckWin ();
				}*/


			}
		}
	}

	void CheckWin(){
		if (hidePlaces[Random.Range (0, 4)] == buffer){
			Debug.Log ("you lost");
		}
		else {
			Debug.Log ("you win");
		}
	}
}

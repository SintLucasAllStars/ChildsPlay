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

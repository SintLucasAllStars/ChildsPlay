using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeek : MonoBehaviour {

	// Use this for initialization
	void Start (){
		
	}
	
	// Update is called once per frame
	void Update (){
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.name == "Hide 1") {
					Debug.Log ("jeej");
				}
				if (hit.transform.name == "Hide 2") {
					Debug.Log ("jeej");
				}
				if (hit.transform.name == "Hide 3") {
					Debug.Log ("jeej");
				}
				if (hit.transform.name == "Hide 4") {
					Debug.Log ("jeej");
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour {
	GameObject playerObject;
	public float openRange;
	public float openSpeed;
	public GameObject doorObject;
	Vector3 doorBegin;
	public Transform doorEnd;
	// Use this for initialization
	void Start () {
		playerObject = GameObject.Find ("player");
		doorBegin = doorObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, playerObject.transform.position) < openRange) {
			doorObject.transform.position = Vector3.MoveTowards (doorObject.transform.position, doorEnd.position, openSpeed * Time.deltaTime);
		} else {
			doorObject.transform.position = Vector3.MoveTowards (doorObject.transform.position, doorBegin, openSpeed * Time.deltaTime);

		}
	}
}

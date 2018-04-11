using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	Vector3 startPosition;
	Vector3 endPosition;
	float timeToMove;
	float timeToExecute;
	float startTime;
	float endPositionZValue;
	bool wasOpen;
	enum DoorStates{Open, Closed, Moving }
	DoorStates doorState;



	// Use this for initialization
	void Start () {
		doorState = DoorStates.Open;
	}

	// Update is called once per frame
	void Update () {

		switch (doorState) {
		case (DoorStates.Closed):
			endPositionZValue = -11.5f;
			wasOpen = false;
			break;

		case (DoorStates.Open):
			endPositionZValue = 11.5f;
			wasOpen = true;
			break;

		case (DoorStates.Moving):
			break;
		}



	}

	IEnumerator MoveDoor(){
		while (Time.time < startTime + timeToMove) {
			doorState = DoorStates.Moving;
			transform.position = Vector3.Lerp (startPosition, endPosition, (Time.time - startTime) / timeToMove);
			yield return null;

		}
		if (wasOpen == true) {
			doorState = DoorStates.Closed;
		} else {
			doorState = DoorStates.Open;
		}
	}

	void OnTriggerStay(Collider coll){
		if (Input.GetKey (KeyCode.F) && (doorState != DoorStates.Moving) ) {
			startPosition = transform.position;
			timeToMove = 2f;
			timeToExecute = timeToMove;
			endPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z + endPositionZValue);
			startTime = Time.time;

			StartCoroutine (MoveDoor ());

		}
	}

}


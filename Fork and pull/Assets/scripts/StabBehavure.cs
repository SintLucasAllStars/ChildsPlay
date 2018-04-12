using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabBehavure : MonoBehaviour {
	public float stabSpeed;
	Transform stabGoal;
	Transform stabStart;
	float stabReady = 0f;
	bool pullingBack = false;

	
	// Use this for initialization
	void Start () {
		stabGoal = GameObject.Find("/player/Main Camera/ForkGoal").transform;
		stabStart = GameObject.Find("/player/Main Camera/ForkStart").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (stabReady < Time.time &&Input.GetMouseButtonDown (0)) {
			stabReady = Time.time + 0.5f;
			pullingBack = false;
		}

		if (transform.localPosition.z >= stabGoal.localPosition.z-0.00001f) {
			pullingBack = true;
		}
			
		if (!pullingBack) {
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, stabGoal.localPosition, stabSpeed * Time.deltaTime);
		} else if (pullingBack) {
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, stabStart.localPosition, stabSpeed/2 * Time.deltaTime);
		}


	}
}

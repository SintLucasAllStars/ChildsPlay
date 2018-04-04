using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabBehaviour : MonoBehaviour {
	public float stabSpeed;
	Transform stabGoal;
	Transform stabStart;
	float stabReady = 0f;
	bool pullingBack = false;
	BoxCollider coll;

	
	// Use this for initialization
	void Start () {
		coll = gameObject.GetComponent<BoxCollider> ();
		coll.enabled = false;
		stabGoal = GameObject.Find("/player/Main Camera/ForkGoal").transform;
		stabStart = GameObject.Find("/player/Main Camera/ForkStart").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (stabReady < Time.time &&Input.GetMouseButtonDown (0)) {
			stabReady = Time.time + 0.5f;
			pullingBack = false;
			coll.enabled = true;
		}

		if (transform.localPosition.z >= stabGoal.localPosition.z-0.00001f) {
			pullingBack = true;
			coll.enabled = false;
		}
			
		if (!pullingBack) {
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, stabGoal.localPosition, stabSpeed * Time.deltaTime);
		} else if (pullingBack) {
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, stabStart.localPosition, stabSpeed/2 * Time.deltaTime);
		}


	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float forwardSpeed;
	public float sidewaySpeed;
	Rigidbody rb;
	float currentForSpeed;
	float currentSideSpeed;
	public float sprintMultiplier;
	float currentSprintMulti;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		

		if (Input.GetKey (KeyCode.LeftShift)) {
			currentSprintMulti = sprintMultiplier;
		} else {
			currentSprintMulti = 1f;
		}
		if (Input.GetKey (KeyCode.W)) {
			currentForSpeed = forwardSpeed*currentSprintMulti;
		} else if (Input.GetKey (KeyCode.S)) {
			currentForSpeed = -forwardSpeed;
		} else {
			currentForSpeed = 0f;
		}

		if (Input.GetKey (KeyCode.D)) {
			currentSideSpeed = sidewaySpeed*currentSprintMulti;
		} else if (Input.GetKey (KeyCode.A)) {
			currentSideSpeed = -sidewaySpeed*currentSprintMulti;
		} else {
			currentSideSpeed = 0f;
		}

		
		rb.velocity = transform.forward * currentForSpeed + transform.right * currentSideSpeed;
		}
	}


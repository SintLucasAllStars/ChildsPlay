using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	float moveSpeed;
	float mouseX;
	float mouseY;
	float rotAmountX;
	float rotAmountY;
	Vector3 targetRotation;
	public float mouseSensitivity;

	// Use this for initialization
	void Start () {
		moveSpeed = 20f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (0f, 0f, moveSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (0f, 0f, -moveSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (-moveSpeed * Time.deltaTime, 0f, 0f);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (moveSpeed * Time.deltaTime, 0f, 0f);
		}

		RotateCamera ();

	}

	void RotateCamera(){
		mouseX = Input.GetAxis ("Mouse X");
		mouseY = Input.GetAxis ("Mouse Y");

		rotAmountX = mouseX * mouseSensitivity;
		rotAmountY = mouseY * mouseSensitivity;

		targetRotation = transform.rotation.eulerAngles;
		targetRotation.x -= rotAmountY;
		targetRotation.y += rotAmountX;

		transform.rotation = Quaternion.Euler (targetRotation);
	}
}



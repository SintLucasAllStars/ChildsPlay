using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	float moveSpeed;
	public Camera myCamera;
	Vector3 aimPos;
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
		aimPos = myCamera.ScreenToWorldPoint (Input.mousePosition);
		aimPos.z = 0;
		transform.rotation = aimPos;
	}
}

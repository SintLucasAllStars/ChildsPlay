using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	float moveSpeed;
<<<<<<< HEAD
	public Camera myCamera;
=======
>>>>>>> f1584255a85629c3991cd84d0b716594dd04001d
	// Use this for initialization
	void Start () {
		moveSpeed = 20f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.E)) {
			transform.eulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y + moveSpeed * Time.deltaTime, transform.localEulerAngles.z);
		}
		if (Input.GetKey (KeyCode.Q)) {
			transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y + -moveSpeed * Time.deltaTime, transform.localEulerAngles.z);
		}
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
<<<<<<< HEAD
		
=======


>>>>>>> f1584255a85629c3991cd84d0b716594dd04001d
	}
}

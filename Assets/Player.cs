using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {




	bool isTagger = true;
	Rigidbody rb;
	float speed = 6f;
	float mouseSense= 3f;

	void Awake () {
		Cursor.lockState = CursorLockMode.Locked;

		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		float horMove = Input.GetAxis("Horizontal");
		float verMove = Input.GetAxis("Vertical");
		//rb.velocity = new Vector3(horMove, 0f,verMove).normalized   * speed;

		rb.velocity = transform.forward * verMove + transform.right * horMove;

		transform.Rotate (0, Input.GetAxis ("Mouse X") * mouseSense, 0f);
		transform.GetChild (0).Rotate (-Input.GetAxis("Mouse Y") * mouseSense,0f,0f);
	}
}

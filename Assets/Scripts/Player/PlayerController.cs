using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 200000;

	Rigidbody rb;
	Vector3 direction;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		float horizontalMovement = Input.GetAxis ("Horizontal");
		float verticalMovement = Input.GetAxis ("Vertical");

		direction = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
	}

	void FixedUpdate () {
		Movement ();
	}
	void Movement () {
		rb.velocity = direction * speed * Time.deltaTime;
	}
}

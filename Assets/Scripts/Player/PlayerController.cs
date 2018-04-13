using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed;

	Rigidbody rb;
	Vector3 direction;

	void Start () {
		speed = 1000;
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		float horizontalMovement = Input.GetAxisRaw ("Horizontal");
		float verticalMovement = Input.GetAxisRaw ("Vertical");

		direction = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
	}

	void FixedUpdate () {
		rb.velocity = direction * speed * Time.deltaTime;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 1;

	Rigidbody rb;
	Vector3 direction;

	void Start () {
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

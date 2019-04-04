using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
	//controller
	private CharacterController charaCon;

	//modifiers
	public float walkSpeed;
	public float runSpeed;
	public float sensivityX;
	public float sensivityY;

	//movement
	private float speed;
	private Vector3 moveDir;

	//look rotation
	private float minimumY = -60f;
	private float maximumY = 60f;
	private float rotationY = 0f;

	private void Awake()
	{
		charaCon = GetComponent<CharacterController>();
	}

	private void Update()
	{
		Move();
		Look();
	}

	private void Move()
	{
		speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
		float moveHor = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		float moveVer = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		moveDir = new Vector3(moveHor, 0f, moveVer);
		moveDir = transform.TransformDirection(moveDir);

		charaCon.Move(moveDir);
	}

	private void Look()
	{
		rotationY += Input.GetAxis("Mouse Y") * sensivityY;
		rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

		transform.eulerAngles = new Vector3(-rotationY, transform.eulerAngles.y + Input.GetAxis("Mouse X") * sensivityX, 0f);
	}
}

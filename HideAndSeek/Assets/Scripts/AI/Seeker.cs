using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
	//controller
	private CharacterController charaCon;
	private BoxCollider col;

	//modifiers
	public float walkSpeed;
	public float runSpeed;
	private float Gravity = 9.81f;
	public float sensivityX;
	public float sensivityY;
	public float maxStamina = 100f;
	private float stamina = 100;
	private float timer;
	private float cooldown = 1.3f;
	private bool isRunning = false;
	private float attackTimer;
	private float attackCooldown = 1.6f;

	//movement
	private float speed;
	private Vector3 moveDir;
	private float Stamina
	{
		get
		{
			return stamina;
		}
		set
		{
			if(stamina != value)
			{
				stamina = value;
				UIManager.Instance.OnStaminaChanged(stamina);
			}
		}
	}

	//look rotation
	private float minimumY = -60f;
	private float maximumY = 60f;
	private float rotationY = 0f;

	private void Awake()
	{
		charaCon = GetComponent<CharacterController>();
		col = GetComponent<BoxCollider>();
		col.enabled = false;
	}

	private void Update()
	{
		Move();
		Look();

		if (Input.GetMouseButtonDown(0) && attackTimer >= attackCooldown)
		{
			Attack();
			attackTimer = 0f;
		}
		else
		{
			attackTimer += Time.deltaTime;
		}

		if(isRunning)
		{
			Stamina -= ((100f / 6f) * Time.deltaTime);
			timer = 0f;
		}
		else
		{
			if(timer >= cooldown)
			{
				Stamina += ((100f / 6f) * Time.deltaTime);
			}
			else
			{
				timer += Time.deltaTime;
			}
		}
	}

	private void Move()
	{
		speed = walkSpeed;
		isRunning = false;
		if(Input.GetKey(KeyCode.LeftShift) && Stamina >= 0.1f)
		{
			speed = runSpeed;
			isRunning = true;
		}
		float moveHor = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		float moveVer = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		moveDir = new Vector3(moveHor, 0f, moveVer);
		moveDir = transform.TransformDirection(moveDir);
		moveDir.y = 0f;

		charaCon.Move(moveDir);
	}

	private void Look()
	{
		rotationY += Input.GetAxis("Mouse Y") * sensivityY;
		rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

		transform.eulerAngles = new Vector3(-rotationY, transform.eulerAngles.y + Input.GetAxis("Mouse X") * sensivityX, 0f);
	}

	private void LateUpdate()
	{
		Stamina = Mathf.Clamp(Stamina, 0f, maxStamina);
	}

	private void Attack()
	{
		StartCoroutine(AttackCollider());
	}

	private IEnumerator AttackCollider()
	{
		col.enabled = true;
		yield return new WaitForSeconds(1.5f);
		col.enabled = false;
	}
}

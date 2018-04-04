using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature_Maneger : MonoBehaviour {

	//OOP part
	private AI_Class aI_Class;

	public AI_Class.Type TypeKid;
	[SerializeField] public static int Chasers;
	private bool isChaser;
	public float stamina;
	private float baseStamina;	
	public float speed;
	public float reactionSpeed;
	public float fov;
	
	//AI
	public enum State{Panic, Scarecrow, running, walking, hiding}
	private State myState;
	private NavMeshAgent agent;
	private Transform[] hidingplaces;
	public Vector3 target;

	// Use this for initialization
	void Start () {
		aI_Class = new AI_Class(1);
		agent = GetComponent<NavMeshAgent>();
		agent.enabled = true;
		TargetUpdate();
		GetOOp();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			TargetUpdate();
		}
		RaycastHit hit;
		Vector3 direction = target - transform.position;
		if (Physics.Raycast(transform.position, direction, out hit))
		{
			if (hit.transform.gameObject.CompareTag("Wall"))
			{
				float angleW = Vector3.Angle(transform.forward,direction);
				if (fov < angleW / 2)
				{
					//targetupdate should be called	
				}
			}
			if (hit.transform.gameObject.CompareTag("Tagger"))
			{
				float angleT = Vector3.Angle(transform.forward, direction);
				if (fov < angleT / 2)
				{
					myState = State.Panic;
					//a panic mode should be set
				}
			}
		}
	}

	void Stamina()
	{
		if (stamina <= 0)
		{
			myState = State.walking;
			agent.speed = speed / 2;
		}
		
		while (myState == State.walking)
		{
			stamina += Time.deltaTime * 3.5f;
			if (stamina >= baseStamina)
			{
				myState = State.running;
				agent.speed = speed;
			}
		}
		while (myState == State.running)
		{
			stamina -= Time.deltaTime * 1.5f; 
		}
	}

	void Panic()
	{
		switch (myState)
		{
			case State.Panic:
				//what should happen when he panics
			break; 
			default:
				Debug.Log("the player is not in the right state");
			break;
		}
	}

	void TargetUpdate()
	{
		Debug.Log("i have been called");
		Debug.Log(agent.isOnNavMesh);
		if(agent.isOnNavMesh)
		{
			target = new Vector3(Random.Range(0,250),0,Random.Range(0,250));			
			agent.SetDestination(target);
			//Debug.Log(target);
		}
		else
		{
			return;
		}
	}

	void GetOOp()
	{
		TypeKid = aI_Class.TypeKid;
		isChaser = aI_Class.isChaser;
		stamina = aI_Class.stamina;
		speed = aI_Class.speed;
		agent.speed = speed;
		reactionSpeed = aI_Class.reactionSpeed;
		fov = aI_Class.fov;
	}
}

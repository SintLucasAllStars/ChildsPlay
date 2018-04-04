using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature_Maneger : MonoBehaviour {

	//scripts needed acces
	private TerrainGenerator terrainGenerator;

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
		terrainGenerator = GameObject.Find("World").GetComponent<TerrainGenerator>();
		TargetUpdate();
		GetOOp();
	}
	
	// Update is called once per frame
	void Update () 
	{
		TargetUpdate();
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

	void CollisionDetection()
	{		
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

	void TargetUpdate()
	{
		Debug.Log("i have been called");
		Debug.Log(agent.isOnNavMesh);
		if (agent.isOnNavMesh)
		{
			if(agent.remainingDistance <= 0)
			{
				float x = Random.Range(0,250);
				float z = Random.Range(0,250);
				float y = terrainGenerator.ReturnHeight(x,z);
				target = new Vector3(x,y,z);	
				Debug.Log(target);		
				agent.SetDestination(target);

			}
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

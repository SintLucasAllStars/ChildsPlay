using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature_Maneger : MonoBehaviour {

	//OOP Declared
	private AI_Class ai_Class;
	private bool isChaser;
	private float stamina;
	private float reactionSpeed;
	private float fov;

	//AI
	private bool onGround;
	private NavMeshAgent agent;
	public Vector3 target;
	[SerializeField] private Vector3 currentPos;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.enabled = true;		
		TarUpdate();
	}
	
	// Update is called once per frame
	void Update () {	
		if (Input.GetKey(KeyCode.A))
		{
			TarUpdate();
		}					
		agent.SetDestination(target);
		Debug.Log("uodateing destination");
			
		/*
		//currentPos = transform.position;
		if (currentPos.x == target.x && currentPos.z == target.z)
		{
			TarUpdate();
		}
		*/		
	}	

	void TarUpdate()
	{
		target = new Vector3(Random.Range(0,250),0,Random.Range(0,250));			
		if (agent.isOnNavMesh)
		{	
			Debug.Log( agent.destination);
		}
	}
	
}

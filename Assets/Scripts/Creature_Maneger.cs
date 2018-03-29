using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature_Maneger : MonoBehaviour {

	//OOP part
	private AI_Class aI_Class;

	public AI_Class.Type TypeKid;
	public bool isChaser;
	public float stamina;
	public float reactionSpeed;
	public float fov;
	
	//AI
	public enum State{Panic, Scarecrow, running, hiding}
	private State myState;
	private NavMeshAgent agent;
	private Transform[] hidingplaces;
	private Vector3 target;

	// Use this for initialization
	void Start () {
		aI_Class = new AI_Class(1);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
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
		target = new Vector3(Random.Range(5,-5),0.5f,Random.Range(5,-5f));
		agent.SetDestination(target);
	}

	void GetOOp()
	{
		TypeKid = aI_Class.TypeKid;
		isChaser = aI_Class.isChaser;
		stamina = aI_Class.stamina;
		reactionSpeed = aI_Class.reactionSpeed;
		fov = aI_Class.fov;
	}
}

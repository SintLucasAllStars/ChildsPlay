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
	[SerializeField] private Vector3 target;
	[SerializeField] private Vector3 currentPos;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.enabled = true;
		StartCoroutine(FirstTarUpdate());
	}
	
	// Update is called once per frame
	void Update () {		
		//currentPos = transform.position;
		if (currentPos.x == target.x && currentPos.z == target.z)
		{
			TarUpdate();
		}
		agent.SetDestination(target);
		Debug.Log(target);
		Debug.Log(agent.destination);
				
	}	

	void TarUpdate()
	{
		target = new Vector3(Random.Range(0,250),0,Random.Range(0,250));
		if (agent.isOnNavMesh)
		{			
			agent.SetDestination(target);
		}
	}

	IEnumerator FirstTarUpdate()
	{		
		yield return new WaitForSeconds(2);
		target = new Vector3(Random.Range(0,250),0,Random.Range(0,250));
		if (agent.isOnNavMesh)
		{	
			Debug.Log("moving");		
			agent.SetDestination(target);
		}
	}
}

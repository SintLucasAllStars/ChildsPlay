using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pathfinding_test : MonoBehaviour {

	private NavMeshAgent agent;
	private Vector3 target = new Vector3(50,10,120);

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.enabled = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (agent.isOnNavMesh)
		{
			agent.SetDestination(new Vector3(10,50));
		}
	}
}

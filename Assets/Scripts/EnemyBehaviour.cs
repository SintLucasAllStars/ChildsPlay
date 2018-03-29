using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour {

	NavMeshAgent agent;
	public bool isTagger;
	GameObject target;

	void Awake() {
		agent = GetComponent<NavMeshAgent> ();
	}

	void Update () {
		if (isTagger)
			agent.SetDestination (target.transform.position);
		else
			agent.SetDestination ((transform.position - target.transform.position).normalized);
	}
}

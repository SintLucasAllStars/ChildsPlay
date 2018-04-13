using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : AIBehaviour {
	public enum GuardMode {Normal, Search, Chase};
	public GuardMode guardMode;

	public float searchSpeed;
	public float chaseSpeed;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		bodyType = (BodyType)Random.Range (0, 2);
		guardMode = GuardMode.Normal;

		switch (bodyType) {
		case BodyType.Fat:
			normalSpeed = 2f;
			searchSpeed = 2f;
			chaseSpeed = 3f;
			transform.localScale = new Vector3 (2.5f, 1.8f, 2.5f);
			break;
		case BodyType.Normal:
			normalSpeed = 2.5f;
			searchSpeed = 3f;
			chaseSpeed = 5f;
			transform.localScale = new Vector3 (1.8f, 1.8f, 1.8f);
			break;
		case BodyType.Aggressive:
			normalSpeed = 2.5f;
			searchSpeed = 4f;
			chaseSpeed = 6f;
			transform.localScale = new Vector3 (1.8f, 2f, 1.8f);
			break;
		}
		SetMode ();
		agent.SetDestination (centralIntelligence.MoveToSector (1));
	}

	void Update () {
		
	}

	public void SetMode () {
		switch (guardMode) {
		case GuardMode.Normal:
			agent.speed = normalSpeed;

			break;
		case GuardMode.Search:
			agent.speed = searchSpeed;

			break;
		case GuardMode.Chase:
			agent.speed = chaseSpeed;

			break;
		}
	}
}

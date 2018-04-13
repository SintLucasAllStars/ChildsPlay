using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : AIBehaviour {
	public enum GuardMode {Normal, Search, Chase};
	public GuardMode guardMode;

	RaycastHit hit;

	Vector3 lastSeen;

	public float searchSpeed;
	public float chaseSpeed;
	public float fov;

	public float changeSectorCounter;
	public float changeSectorTime;

	int sectorCount;
	int currentSector;

	bool searchPlayer;
	bool seePlayer;

	public Transform player;


	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		bodyType = (BodyType)Random.Range (0, 3);
		guardMode = GuardMode.Normal;
		sectorCount = centralIntelligence.sectorVectors.GetLength(0);
		changeSectorTime = Time.time + Random.Range (15, 30);
		changeSectorCounter = changeSectorTime;
		searchPlayer = false;

		switch (bodyType) {
		case BodyType.Fat:
			normalSpeed = 2f;
			searchSpeed = 2f;
			chaseSpeed = 3f;
			fov = 90;
			transform.localScale = new Vector3 (2.5f, 1.8f, 2.5f);
			break;
		case BodyType.Normal:
			normalSpeed = 2.5f;
			searchSpeed = 3f;
			chaseSpeed = 5f;
			fov = 120;
			transform.localScale = new Vector3 (1.8f, 1.8f, 1.8f);
			break;
		case BodyType.Aggressive:
			normalSpeed = 2.5f;
			searchSpeed = 4f;
			chaseSpeed = 6f;
			fov = 180;
			transform.localScale = new Vector3 (1.8f, 2f, 1.8f);
			break;
		}
		SetMode (GuardMode.Normal);
	}

	void Update () {
		seePlayer = SeePlayer ();

		switch (guardMode) {
		case GuardMode.Normal:
			changeSectorCounter += Time.deltaTime;
			if (agent.remainingDistance < 0.5f)
				WalkInSector ();
			if (changeSectorTime < changeSectorCounter) {
				SelectSector ();
				changeSectorTime = Time.time + Random.Range (30, 60);
			}
			if (searchPlayer == true) {
				SetMode (GuardMode.Chase);
			}
			break;
		case GuardMode.Search:
			//get the latest seen position of the player and set the destination on that position.
			break;
		case GuardMode.Chase:
			//get the position of the player and set the destination on that position.
			break;

		}
	}

	public void SetMode (GuardMode gm) {
		switch (gm) {
		case GuardMode.Normal:
			guardMode = GuardMode.Normal;
			agent.speed = normalSpeed;

			break;
		case GuardMode.Search:
			guardMode = GuardMode.Search;
			agent.speed = searchSpeed;

			break;
		case GuardMode.Chase:
			guardMode = GuardMode.Chase;
			agent.speed = chaseSpeed;

			break;
		}
	}

	public void SelectSector () {
		currentSector = Random.Range (0, sectorCount);
		agent.SetDestination (centralIntelligence.MoveToSector (currentSector));
	}

	public void WalkInSector () {
		agent.SetDestination (centralIntelligence.MoveToSector (currentSector));
	}
	public bool SeePlayer () {
		Vector3 direction = player.position - transform.position;
		if (Physics.Raycast (transform.position, direction, out hit)) {
			if (hit.collider.gameObject.CompareTag ("Player")) {
				float angle = Vector3.Angle (transform.forward, direction);
				if (angle < fov) {
					return true;
				} else {
					return false;
				}
			} else {
				return false;
			}
		} else {
			return false;
		}
	}
}

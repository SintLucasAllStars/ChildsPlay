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

	public float stopSearch;

	int sectorCount;
	int currentSector;

	bool searchPlayer;
	bool seePlayer;

	public Transform player;


	void Start () {

		agent = GetComponent<NavMeshAgent> ();

		bodyType = (BodyType)Random.Range (0, 3);

		sectorCount = centralIntelligence.sectorVectors.GetLength(0);
		changeSectorTime = Time.time + Random.Range (15, 30);
		changeSectorCounter = changeSectorTime;

		player = GameObject.FindGameObjectWithTag ("Player").transform;
		searchPlayer = true;

		switch (bodyType) {
		case BodyType.Fat:
			normalSpeed = 2f;
			searchSpeed = 2f;
			chaseSpeed = 3f;
			fov = 60;
			transform.localScale = new Vector3 (2.5f, 1.8f, 2.5f);
			break;
		case BodyType.Normal:
			normalSpeed = 2.5f;
			searchSpeed = 3f;
			chaseSpeed = 5f;
			fov = 90;
			transform.localScale = new Vector3 (1.8f, 1.8f, 1.8f);
			break;
		case BodyType.Aggressive:
			normalSpeed = 2.5f;
			searchSpeed = 4f;
			chaseSpeed = 6f;
			fov = 120;
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
			if (agent.remainingDistance < 0.5f) {
				WalkInSector ();
			}
			if (changeSectorTime < changeSectorCounter) {
				SelectSector ();
				changeSectorTime = Time.time + Random.Range (15, 30);
			}
			if (seePlayer == true && searchPlayer == true) {
				SetMode (GuardMode.Chase);
			}
			break;
		case GuardMode.Search:
			if (Time.time > stopSearch) {
				SetMode (GuardMode.Normal);
			}
			if (agent.remainingDistance < 0.5) {
				agent.SetDestination (new Vector3 (transform.position.x + Random.Range (-10, 10), transform.position.y + Random.Range (-10, 10), transform.position.z + Random.Range (-10, 10)));
			}
			if (seePlayer) {
				SetMode (GuardMode.Chase);
			}
			break;
		case GuardMode.Chase:
			agent.SetDestination (player.position);
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
			agent.SetDestination (centralIntelligence.GetLastPosition ());
			stopSearch = Time.time + Random.Range (30, 45);

			break;
		case GuardMode.Chase:
			AlertGuards ();
			guardMode = GuardMode.Chase;
			agent.speed = chaseSpeed;

			break;
		}
	}

	public void SelectSector () {
		currentSector = Random.Range (0, sectorCount);
		agent.SetDestination (centralIntelligence.MoveToSector (currentSector));
		Debug.Log ("desination Set");
	}

	public void WalkInSector () {
		agent.SetDestination (centralIntelligence.MoveToSector (currentSector));
		Debug.Log ("desination Set in Sector");
	}

	public void AlertGuards () {
		centralIntelligence.Alert (player.position);
	}

	public void Alerted () {
		SetMode (GuardMode.Search);
		searchPlayer = true;
	}

	public bool SeePlayer () {
		Vector3 direction = player.position - transform.position;
		if (Physics.Raycast (transform.position, direction, out hit)) {
			if (hit.collider.gameObject.CompareTag ("Player")) {
				float angle = Vector3.Angle (transform.forward, direction);
				if (angle < fov) {
					Debug.Log ("Can see");
					return true;
				} else {
					return false;
				}
			} else {
				return false;
			}
		} else {
			Debug.Log ("Can't see");
			return false;
		}
	}
}

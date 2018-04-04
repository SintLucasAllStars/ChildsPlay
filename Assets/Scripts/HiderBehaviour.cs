using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HiderBehaviour : MonoBehaviour {

	public enum AIstate {moving,hiding,captured};
	public AIstate currentState;

	public Vector3 startPosition;
	public Vector3 hidingPosition;

	private NavMeshAgent nma;

	private GameObject seeker;

	private GameManager gm;

	public float mapSize;

	void Start () {
		
		seeker = GameObject.FindGameObjectWithTag ("Seeker");
		gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		nma = GetComponent<NavMeshAgent> ();

		startPosition = transform.position;
		hidingPosition = new Vector3 (Random.Range (-mapSize/2, mapSize/2), 1, Random.Range (-mapSize/2, mapSize/2));
		nma.SetDestination (hidingPosition);
		currentState = AIstate.moving;

	}

	void Update () {
		
		switch (currentState) {
		case AIstate.moving:
			Moving ();
			break;
		case AIstate.hiding:
			Hiding ();
			break;
		case AIstate.captured:
			Captured ();
			break;
		default:
			Moving ();
			break;
		}

	}

	public void Moving(){
		
		nma.SetDestination (hidingPosition);
		if (nma.remainingDistance < 0.5f) {
			currentState = AIstate.hiding;
		}

	}

	public void Hiding(){

		RaycastHit hit;
		Debug.DrawRay (transform.position, seeker.transform.position - transform.position);
		if (Physics.Raycast(transform.position,(seeker.transform.position - transform.position),out hit)) {
			Debug.Log ("spotted seeker");
		}

		Vector3 lookAt = Vector3.RotateTowards (transform.position, seeker.transform.position, 10f, 10f);

		transform.rotation = Quaternion.LookRotation (lookAt);

	}

	public void Captured(){
		
		nma.SetDestination (startPosition);

	}

}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HiderBehaviour : MonoBehaviour {

	// Enumerators
	public enum AIstate {moving,hiding,captured};

	[Header("AI State")]
	public AIstate currentState;

	[Header("Set Statistics")]
	public float satisfactionReq;
	public float scanRate;

	[Header("Hiding Statistics")]
	public bool isSatisfied;
	public float concealment;
	public float lowestScan;
	public Vector3 hidingPosition;
	public Quaternion hidingRadian;

	private Vector3 startPosition;

	private RaycastHit hit;

	private NavMeshAgent nma;
	private GameObject seeker;
	private GameManager gm;



	void Start(){

		seeker = GameObject.FindGameObjectWithTag ("Seeker");
		gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		nma = GetComponent<NavMeshAgent> ();

		startPosition = transform.position;
		hidingPosition = new Vector3 (Random.Range (-45, 45), 0, Random.Range (-45, 45));
		nma.SetDestination (hidingPosition);

		StartCoroutine (FindHidingPlace ());

	}

	void Update(){
	
		switch (currentState) {
		case AIstate.moving:
			break;
		case AIstate.hiding:
			FindHidingPlace ();
			break;
		case AIstate.captured:
			nma.SetDestination (startPosition);
			break;
		default:
			break;
		}
	
	}

	IEnumerator FindHidingPlace(){

		while (!isSatisfied) {
			concealment = 0;
			Vector3 dir = Vector3.zero;
			for (int i = 0; i < 10; i++) {
				if (Physics.Raycast (transform.position, dir, out hit, 20f)) {
					concealment += hit.distance;
				} else {
					concealment += 20;
				}
				Debug.DrawRay (transform.position, dir,Color.red);
				dir = Quaternion.AngleAxis(36 * i,transform.up)*transform.forward;
			}
			yield return new WaitForSeconds (scanRate);
		}


	}

}
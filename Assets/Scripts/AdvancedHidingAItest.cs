using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdvancedHidingAItest : MonoBehaviour {

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
	private RaycastHit hit;

	private Vector3 startPosition;

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

	}

	IEnumerator FindHidingPlace(){
	
		while (!isSatisfied) {
			for (int i = 0; i < 10; i++) {

				/*

				Scan 10x, save the best one and save the vector. Then go to it.

				*/

			}
			yield return new WaitForSeconds (scanRate);
		}

	
	}

}
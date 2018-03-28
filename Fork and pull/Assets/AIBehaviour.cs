using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour {
	public enum Mode {seek, hide, chase};

	NavMeshAgent nav;
	public Transform target;
	public Mode mode;


	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		SetMode (Mode.seek);
		StartCoroutine (Seekmode ());
	}

	void Update () {

		switch (mode) {

		case Mode.seek:
			
		break;
		
		case Mode.hide:

		break;
		
		case Mode.chase:

		break;

		}
		
	}



	void SetMode (Mode m){
		mode = m;
	}

	IEnumerator Seekmode () {
		while (true) {
			if (mode == Mode.seek) {
				Vector3 destination = transform.position + new Vector3 (Random.Range (-10, 10), 1, Random.Range (-10, 10));
				nav.SetDestination (destination);
				yield return new WaitForSeconds (Random.Range (1, 4));
			}
		}
	}
}

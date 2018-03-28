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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshProblem : MonoBehaviour
{
    private NavMeshAgent nav;

    public Transform hidingspot;
	// Use this for initialization
	void Start () {
	    nav = nav = GetComponent<NavMeshAgent>();
	    nav.destination = hidingspot.transform.position;

	}

    // Update is called once per frame
    void Update () {
		
	}
}

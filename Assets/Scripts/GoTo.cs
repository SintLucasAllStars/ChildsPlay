using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoTo : MonoBehaviour {
    public GameObject[] spots;
    NavMeshAgent nav;
    Transform Spot;

	// Use this for initialization
	void Start () {
        Spot = GameObject.FindGameObjectWithTag("Destination").transform;
        nav = GetComponent<NavMeshAgent>();
        nav.destination = Spot.position;
	}
	
	// Update is called once per frame
	void Update () {
//
//        Vector3 destination = transform.position + new Vector3(Random.Range(-100, 100), 0f, Random.Range(-100, 100));
//        nav.SetDestination(destination);
//        {
//            new WaitForSeconds(5);
//        }
	}
}

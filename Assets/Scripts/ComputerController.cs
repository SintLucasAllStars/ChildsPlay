﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ComputerController : MonoBehaviour {

    public Camera cam;

    public Transform goal;

	// Use this for initialization
	void Start ()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}

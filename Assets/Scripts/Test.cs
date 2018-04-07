using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : EddieBehaviour {

	void LateAwake()
	{
		Debug.Log("I'm late...");
	}

	// Use this for initialization
	void Start () {
		Debug.Log("Start");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

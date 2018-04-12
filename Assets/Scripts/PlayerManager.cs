using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	float range;
	Vector3 target;
	/*
	speed
	stamina

	
	
	*/

	public enum State {idle, Walk, Run, Exhausted}
	// Use this for initialization
	void Start () 
	{
		range = 12f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.E))
		{
			Debug.Log("pressed E");
			CheckForBush();
		}
		
	}

	void CheckForBush()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.forward, out hit, range))
		{
			Debug.Log(hit.transform.tag);
			if (hit.transform.tag == "Hidingspot")
			{
				target = hit.transform.position;
				Hide();
			}
		}
	}

	void Hide()
	{
		transform.position = target;
	}
}

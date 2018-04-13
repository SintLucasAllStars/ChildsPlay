using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

	public GameObject hidingObject;

	bool foundHidingSpot;

	float range;
	float speed;
	GameObject target;
	GameObject emptyTarget;
	public Vector3 targetxyz;
	public Vector3 emtpyTargetxyz;


	//UI Elements
	public Text pressToText;

	RaycastHit hit;

	// Use this for initialization
	void Start () 
	{
		range = 12f;
		speed = 7f;


		pressToText.text = " ";
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Physics.Raycast(transform.position, transform.forward, out hit, range))
		{
			CheckForHidingSpot();
			if(Input.GetKey(KeyCode.E))
			{
				Debug.Log("pressed E");
				
			}
		}
			
		//Moves to targets position when there is a target.		#Jesper
		if(target != null)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetxyz, speed * Time.deltaTime);
		}
		//When on hiding position, displays option to leave hiding spot.	#Jesper
		if (transform.position == targetxyz)
		{
			pressToText.text = "Press E to leave";
			if(Input.GetKey(KeyCode.E))
			{
				ResetHidingSpot();
				LeaveHidingSpot();
				ColliderOn();
			}
		}		
	}

	//Raycast that checks for hidingspots.	#Jesper
	void CheckForHidingSpot()
	{
			if (hit.transform.tag == "Hidingspot")
			{
				pressToText.text = "Press E to Hide";
				if(Input.GetKey(KeyCode.E))
				{
					Debug.Log(hit.transform.tag);
					hidingObject = hit.transform.gameObject;
					ColliderOff();
					SetHidingSpot();
				}
				
			}
			else
			{
				Debug.Log("Keep Looking");
			}
	}

	//Sets hidingspot you see as target.	#Jesper
	void SetHidingSpot()
	{
		target = hidingObject;
		targetxyz = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
	}
	//Empties your target.	#Jesper
	void ResetHidingSpot()
	{
		
		target = emptyTarget;
		targetxyz = emtpyTargetxyz;
	}

	//Turns collider of the hidingspot off so "Player" can hide in the hidingspot.	#Jesper
	void ColliderOff()
	{
		Collider tempCollider;

		tempCollider = hidingObject.GetComponent<Collider>();

		tempCollider.enabled = false;
	}
	//Turns Collider of the hidingspot back on so "Player" can hide again in the hidingspot.	#Jesper
	void ColliderOn()
	{
		Collider tempCollider;

		tempCollider = hidingObject.GetComponent<Collider>();

		tempCollider.enabled = true;
	}
	
	//I tried making a bool of both the ColliderOff() and ColliderOn() methods but didn't manage to make it work//


	//
	void LeaveHidingSpot()
	{
		for(int i = 0; i < 15; i++)
		{
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}
	}


	
}

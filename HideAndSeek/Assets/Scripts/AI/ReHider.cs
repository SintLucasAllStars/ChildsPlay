using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReHider : Humanoid
{
	private AEnvQuerySystem EQS;
	private GameObject Seeker;

	private bool seekerSeen = false;

	private void Start()
	{
		EQS = AEnvQuerySystem.Instance;
		Seeker = GameObject.FindGameObjectWithTag("Seeker");
		FindNewLocation();
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			FindNewLocation();
		}
		LookForSeeker();
	}

	private void FindNewLocation()
	{
		Debug.Log(EQS.hideLocations.Count);
		int index = Random.Range(0, EQS.hideLocations.Count);
		Vector3 newLoc = EQS.hideLocations[index].GetWorldLocation();
		MoveTo(newLoc);
	}

	private void LookForSeeker()
	{
		Ray ray = new Ray(transform.position + new Vector3(0, 1, 0), (Seeker.transform.position - transform.position));
		if (seekerSeen == false)
		{
			if (Physics.Raycast(ray, out RaycastHit hit, 10))
			{
				if (hit.collider.CompareTag("Seeker"))
				{
					seekerSeen = true;
					FindNewLocation();
				}
				else
				{
					seekerSeen = false;
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour {
	public enum Mode {seek, hide, chase};

	NavMeshAgent nav;
	public Transform target;
	public Mode mode;

	public float fov = 120;
	public float delay;


	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		SetMode (Mode.seek);
		StartCoroutine (Seekmode ());
	}

	void Update () {
		if (canSeeTarget ()) {
			mode = Mode.chase;
		} else
			mode = Mode.seek;

		switch (mode) {

		case Mode.seek:
			
		break;
		
		case Mode.hide:

		break;
		
		case Mode.chase:
			nav.SetDestination (target.position);
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
			} else
				yield return false;
		}
	}
	bool canSeeTarget()
	{
		RaycastHit hit;

		Vector3 direction = target.position - transform.position;
		if(Physics.Raycast(transform.position, direction, out hit))
		{
			if(hit.collider.gameObject.CompareTag("Player"))
			{
				float angle = Vector3.Angle(transform.forward, direction);
				if(angle < fov/2)
				{
					delay -= 1 * Time.deltaTime;
					if(delay < 0)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			} else 
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}


		
}

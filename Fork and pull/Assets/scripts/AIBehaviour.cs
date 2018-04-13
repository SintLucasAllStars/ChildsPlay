using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AIBehaviour : MonoBehaviour {
	public enum Mode {seek, hide, chase};

	NavMeshAgent nav;
	public Transform target;
	public Mode mode;
    public Transform player;
    float runDistance;
	public float fov = 120;
	public float delay;

	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		SetMode (Mode.seek);
		StartCoroutine (Seekmode ());
	}

	void Update () {
		
		if (canSeeTarget ()) {
            if (!PlayerLook.hasFork)
            {
                mode = Mode.chase;
            }
		} else {
            if (!PlayerLook.hasFork)
            {
                mode = Mode.seek;
            } else
            {
                mode = Mode.hide;
            }
        }

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
        while (true)
        {
            switch (mode)
            {

                case Mode.seek:
                    Vector3 destination = transform.position + new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));
                    nav.SetDestination(destination);
                    yield return new WaitForSeconds(Random.Range(1, 4));
                    break;

                case Mode.hide:
                    float distance = Vector3.Distance(transform.position, player.transform.position);
                    if(distance < runDistance)
                    {
                        Vector3 dirToPlayer = transform.position - player.transform.position;
                        Vector3 newPos = transform.position + dirToPlayer;
                        nav.SetDestination(newPos);
                    }

                    break;
                case Mode.chase:
                    nav.SetDestination(target.position);
                    break;

            }
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
				delay = 3;
				return false;
			}
		}
		else
		{
			return false;
		}
	}
    Vector3 ReturnPosition(Vector3 pos)
    {
        return pos;
    }


		
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{


	bool tagged = false;

	[SerializeField]
	Transform destination;

	NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start()
	{

		navMeshAgent = this.GetComponent<NavMeshAgent>();


	}

	private void Update()
	{
		if (navMeshAgent == null)
		{
			//Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
		}
		else
		{
			SetDestination();
		}
	}

	private void SetDestination()
	{
		if (destination != null)
		{
			Vector3 targetVector = destination.transform.position;
			if (tagged == true)
			{
				navMeshAgent.SetDestination(-targetVector);
			}
			else
			{
				navMeshAgent.SetDestination(targetVector);
			}

		}
		
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "player")
		{
			Debug.Log("Kanker");
			tagged = true;
			
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Types : MonoBehaviour
{

    NavMeshAgent navMeshAgent;
	public float sprintValue;
	public float walkingSpeed;
    public enum Type
    {
        Tikker,
        NietTikker
	};

	public enum SprintMode
	{
		quickSprint,
		latesprint
	};

	public bool isPlayer;
	public bool canWalk = true;
	public GameObject tikker;
    public Type type;
	public SprintMode sprintMode;
    Vector3 randomPos;

    void Start () {
		type = Type.NietTikker;
		navMeshAgent = GetComponent<NavMeshAgent>();
		if (type == Type.NietTikker) {
			navMeshAgent.speed = walkingSpeed;
			navMeshAgent.SetDestination( new Vector3 (Random.Range (-13, 13), transform.position.y, Random.Range (-13, 13)));
		}
    }

	void Update () {
        Walking();

	}

    void Walking()
    {
        if (canWalk)
        {
            if (navMeshAgent.remainingDistance < 1f)
            {
                randomPos = new Vector3(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100));

                navMeshAgent.SetDestination(randomPos);
            }
        }
    }

	public void SprintBehaviour(){
		switch (sprintMode) {
		case SprintMode.quickSprint:
			
			break;

		case SprintMode.latesprint:
			
			break;

		}
	}

	bool canSeeTarget(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, tikker.transform.position - transform.position, out hit)) 
		{
			if (hit.collider.gameObject.CompareTag ("Tikker")) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}
		
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Types : MonoBehaviour
{

    NavMeshAgent navMeshAgent;
	public float sprintValue;
	float maxSprint;
	public float walkingSpeed;

    public enum Type
    {
        Tagged,
        NotTagged
	};

	public enum SprintMode
	{
		quickSprint,
		latesprint
	};

	public bool isPlayer;
	public bool canWalk = true;
	public bool isSprinting;
	public bool canSprint = true;
	public Type type;
	public SprintMode sprintMode;
    Vector3 randomPos;
	public Transform startPosition;

    public GameObject target;

    void Start () {
		navMeshAgent = GetComponent<NavMeshAgent>();
		sprintValue  = Random.Range(100,160);
		maxSprint = sprintValue;

		if (Random.Range (0, 2) == 1) {
			sprintMode = SprintMode.latesprint;
		} else {
			sprintMode = SprintMode.quickSprint;
		}
	}


    void Update()
    {
        if (!isPlayer)
        {
            if (isSprinting = false && sprintValue < maxSprint)
            {

            }

            if (target != null && target.GetComponent<Types>() != null && target.GetComponent<Types>().type == Type.NotTagged)
            {
                target = null;

            }


            switch (type)
            {
                case Type.Tagged:

                    RaycastHit hit;
                    if (target == null)
                    {
                        if (canWalk && navMeshAgent.remainingDistance < 1f)
                        {
                            SetDestination();
                        }

                    }
                    if (Physics.Raycast(transform.position, transform.forward, out hit, 1000))
                    {
                        if (canSeeTarget(hit.collider.gameObject))
                        {
                            if (hit.collider.gameObject.GetComponent<Types>() != null && hit.collider.gameObject.GetComponent<Types>().type == Type.NotTagged)
                            {
                                target = hit.collider.gameObject;
                                SetDestination(target);
                            }

                        }
                        else
                        {
                            if (canWalk && navMeshAgent.remainingDistance < 1f)
                            {
                                SetDestination();
                            }
                        }

                    }

                    break;

                case Type.NotTagged:

                    //  if (navMeshAgent.remainingDistance < 1f)
                    // {
                    //      SetDest();
                    //  }
                    if (canSeeTarget(GameManager.instance.tikkers[0].gameObject))
                    {
                        RunFrom(GameManager.instance.tikkers[0].gameObject);
                    }
                    else
                    {
                        if (navMeshAgent.remainingDistance < 1f)
                        {
                            SetDestination();
                        }
                    }
                    break;
            }
        }
    }

    void SetDestination()
    {
        if (canWalk)
        {
            randomPos = new Vector3(Random.Range(-13, 13), Random.Range(-13, 13), Random.Range(-13, 13));

            navMeshAgent.SetDestination(randomPos);
        }
    }

    void SetDestination(GameObject dest)
    {
        if (canWalk)
        {
            navMeshAgent.SetDestination(dest.transform.position);
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

	bool canSeeTarget(GameObject target){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, target.transform.position - transform.position, out hit)) 
		{
			if (hit.collider.gameObject	== target) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}

	public void RunFrom(GameObject currentTikker){
		startPosition = transform;
		transform.rotation = Quaternion.LookRotation (transform.position - currentTikker.transform.position);  //gameManager.GetComponent<GameManager> ().tikkers [0].transform.position);
		Vector3 runTo = transform.position + transform.forward * 6;
		NavMeshHit hit;
		NavMesh.SamplePosition (runTo, out hit, 5, 1 << NavMesh.GetAreaFromName ("Walkable")); 
		transform.position = startPosition.position;
		transform.rotation = startPosition.rotation;
		navMeshAgent.SetDestination (hit.position);
		
	}

	void OnCollisionEnter(Collision coll){
		if (type == Type.Tagged) {

            if(coll.gameObject.GetComponent<Types>() != null && coll.gameObject.GetComponent<Types>().type == Type.NotTagged && GameManager.instance.pastCountdown)
            {
                target = null;
                coll.gameObject.GetComponent<Types>().type = Type.Tagged;
                coll.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                Debug.Log(coll.gameObject.name + " is getikt door " + gameObject.name);
                GameManager.instance.nietTikkers.Remove(coll.gameObject.GetComponent<Types>());
                GameManager.instance.tikkers.Add(coll.gameObject.GetComponent<Types>());
                GameManager.instance.gameObject.GetComponent<PlayerCanvasManager>().RemainingUntagged();
            }

		}
	}

	public void Spront() {
		if (sprintValue >1){
			isSprinting = true;
			navMeshAgent.speed = 5;
			sprintValue -= 0.05f;
		}
	}
		
}

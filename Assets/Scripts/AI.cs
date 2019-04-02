using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AI : Person
{
	NavMeshAgent Agent;
	Vector3 TargetPos;
	public QSystem system;
	float speed;

	List<EQSItem> HideLoc = new List<EQSItem>();
    public Vector3[] seekingPoint;

    private void Start()
	{
		system.Check();
		Agent = GetComponent<NavMeshAgent>();
		system = GameObject.FindWithTag("EQS").GetComponent<QSystem>();
        Agent.destination = seekingPoint[Random.Range(0, seekingPoint.Length)];


        //FindLoc();
    }

    private void Update()
    {

        //if ai has gun find person to shoot
        if (gun.activeSelf)
        {
            if (Vector3.Distance(transform.position, Agent.destination) < 3)
            {
                Agent.destination = seekingPoint[Random.Range(0, seekingPoint.Length)];
            }
        }

        
    }

    void FindLoc()
	{
		Vector3 NewLoc;

		foreach (EQSItem item in system.Qitems)
		{
			Debug.Log(item.CanHide);

			if (item.CanHide == true && item.IsColiding == false)
			{
				HideLoc.Add(item);
			}
		}

		if (HideLoc.Count > 0)
		{ 
			int index = Random.Range(0, HideLoc.Count);
			Debug.Log(index);
			NewLoc = HideLoc[index].GetWorldLocation();
			Agent.destination = NewLoc;
		}
	}

    void PlayerInRange()
    {

    }

    protected bool CanSeePlayer()
    {
        RaycastHit hit;
        Gamemanager gm = GameObject.FindObjectOfType<Gamemanager>();
        Transform target = GetClosestEnemy(gm.allPlayers);
        Vector3 rayDirection = target.position - transform.position;

        if ((Vector3.Angle(rayDirection, transform.forward)) <= 90 * 0.5f)
        {
            // Detect if player is within the field of view 
            if (Physics.Raycast(transform.position, rayDirection, out hit, 20))
            {
                return (hit.transform.CompareTag("Player"));
            }
        }

        return false;
    }

    Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }
}

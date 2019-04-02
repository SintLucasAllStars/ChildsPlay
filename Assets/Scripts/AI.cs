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

        //if(function && !hasShot)
        {
            //Shoot(transform);
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

    void Shoot(Transform target)
    {
        Shoot();
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            EquipWeapon(other.gameObject);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("F");
            Die();
        }
    }
}

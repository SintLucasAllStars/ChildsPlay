using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AI : Person
{
	NavMeshAgent Agent;
	Vector3 TargetPos;
	public FOV fov; 
	public QSystem system;
	float speed;
    FOV mFov;

	List<EQSItem> HideLoc = new List<EQSItem>();
    public Vector3[] seekingPoint;

    private void Start()
	{
		system.Check();
        gun.SetActive(false);
        Agent = GetComponent<NavMeshAgent>();
		system = GameObject.FindWithTag("EQS").GetComponent<QSystem>();
        Agent.destination = seekingPoint[Random.Range(0, seekingPoint.Length)];
        mFov = GetComponent<FOV>();

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
        else if(GameObject.FindGameObjectWithTag("Gun") != null)
        {
            Agent.destination = GameObject.FindGameObjectWithTag("Gun").transform.position;
        }
        else if(GameObject.FindGameObjectWithTag("Gun") == null)
        {
            if (Vector3.Distance(transform.position, Agent.destination) < 3)
            {
                Agent.destination = seekingPoint[Random.Range(0, seekingPoint.Length)];
            }
        }

        if(mFov.m_See && !hasShot && gun.activeSelf)
        {
            //Debug.Log("Found player");
            Shoot(transform);
        }
    }

    void FindLoc()
	{
		Vector3 NewLoc;
        foreach (EQSItem item in system.Qitems)
		{
			//Debug.Log(item.CanHide);

			if (item.CanHide == true && item.IsColiding == false)
			{
				HideLoc.Add(item);
			}
		}

		if (HideLoc.Count > 0)
		{ 
			int index = Random.Range(0, HideLoc.Count);
			//Debug.Log(index);
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
        if (other.gameObject.CompareTag("Bullet") && gun.activeInHierarchy == false)
        {
            Die();
        }
    }
}

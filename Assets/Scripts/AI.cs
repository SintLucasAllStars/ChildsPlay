using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AI : MonoBehaviour
{
	NavMeshAgent Agent;
	Vector3 TargetPos;
	public QSystem system;
	float speed;

	List<EQSItem> HideLoc = new List<EQSItem>();

	private void Start()
	{
		Agent = GetComponent<NavMeshAgent>();
		system = GameObject.FindWithTag("EQS").GetComponent<QSystem>();
		foreach (EQSItem item in system.Qitems)
		{
			if (item.CanHide == true && item.IsColiding == false)
			{
				HideLoc.Add(item);
			}


		}
		Agent.SetDestination(HideLoc[Random.Range(0, HideLoc.Count)].GetWorldLocation());
	}


    public void RecieveWeapon()
    {
        //get weapon
    }

}

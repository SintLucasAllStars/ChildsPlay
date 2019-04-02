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
		system.Check();
		Agent = GetComponent<NavMeshAgent>();
		system = GameObject.FindWithTag("EQS").GetComponent<QSystem>();
		FindLoc(); 
		//foreach (EQSItem item in system.Qitems)
		//{
		//	Debug.Log("1");
		//	if (item.CanHide == true && item.IsColiding == false)
		//	{
		//		Debug.Log("2");
		//		HideLoc.Add(item);
		//		Debug.Log(item);
		//	}


		//}
		
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

    public void RecieveWeapon()
    {

    }

}

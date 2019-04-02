using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQSItem 
{
	public Vector3 loc;
	public Transform QT;
	public bool CanHide;
	public bool IsColiding; 

	public Vector3 GetWorldLocation()
	{
		return QT.position + loc;
	}


	public EQSItem(Vector3 NewPos, Transform QPos)
	{
		this.loc = NewPos;
		this.QT = QPos; 
	}


	public bool SeeSeeker(Transform target)
	{
		Ray ray = new Ray(GetWorldLocation(), (target.position - GetWorldLocation()));

		if (Physics.Raycast(ray, out RaycastHit hit, 10))
		{
			if (hit.collider.CompareTag("Seeker"))
			{
				return true; 
			}
		}

		return false; 
	}

}

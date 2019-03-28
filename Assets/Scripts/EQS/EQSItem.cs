using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQSItem 
{
	public Vector3 l;
	public Transform QT;
	public bool EnemyClose;
	public bool IsColliding;

	public Vector3 GetWorldLocation()
	{
		return QT.position + l;
	}


	public EQSItem(Vector3 NewPos, Transform QPos)
	{
		this.l = NewPos;
		this.QT = QPos; 
	}


	public bool RunCheck(Transform enemy)
	{
		Ray ray = new Ray(GetWorldLocation(), (enemy.position - GetWorldLocation()));

		if (Physics.Raycast(ray, out RaycastHit hit, 10))
		{
			if (hit.collider.CompareTag("Player"))
			{
				return true; 
			}
		}

		return false; 
	}

}

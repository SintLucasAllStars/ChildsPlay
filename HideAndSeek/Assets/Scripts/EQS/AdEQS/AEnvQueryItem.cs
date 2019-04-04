using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnvQueryItem
{
	private Vector3 location;
	private int column, row;
	private Transform querier;
	private Transform enemy;

	private bool isNextToWall;
	public bool IsNextToWall
	{
		get
		{
			return isNextToWall;
		}
		set
		{
			isNextToWall = value;
		}
	}

	private bool isEnemyNearby;
	public bool IsEnemyNearby
	{
		get
		{
			isEnemyNearby = CheckForEnemy();
			return isEnemyNearby;
		}
		private set
		{
			Debug.LogWarning("not meant to do that");
		}
	}

	private bool isColliding;
	public bool IsColliding
	{
		get
		{
			isColliding = CheckForCollision();
			return isColliding;
		}
		private set
		{
			Debug.LogWarning("not meant to do that");
		}
	}

	public AEnvQueryItem(Vector3 location, Transform querier, Transform enemy)
	{
		this.location = location;
		this.querier = querier;
		this.enemy = enemy;
		column = (int)location.z + 25;
		row = (int)location.x + 25;
	}

	private bool CheckForEnemy()
	{
		Ray ray = new Ray(GetWorldLocation() + new Vector3(0,1,0), (enemy.position - GetWorldLocation()));

		if(Physics.Raycast(ray, out RaycastHit hit, 10)) //change maxDistance
		{
			if(hit.collider.CompareTag("Seeker"))
			{
				return true;
			}
		}
		return false;
	}

	private bool CheckForCollision()
	{
		if(Physics.CheckSphere(GetWorldLocation(), .25f, LayerMask.NameToLayer("Obstacle")))
		{
			return true;
		}
		return false;
	}

	public Vector3 GetWorldLocation()
	{
		return querier.position + location;
	}
}

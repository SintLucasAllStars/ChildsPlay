using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnvQuerySystem : MonoBehaviour
{
	private IQueryGenerator generator;

	private AEnvQueryItem[,] queryItems;
    public List<AEnvQueryItem> hideLocations = new List<AEnvQueryItem>();

    public Transform querier;
	public Transform enemy;
	public int gridSize;

	private bool isColliding;
	private bool neighboring;
	private bool isEnemyNearby;

	public static AEnvQuerySystem Instance;

	private void Awake()
	{
		/////////////////////
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this);
		}
		/////////////////////

		queryItems = new AEnvQueryItem[gridSize + 1, gridSize + 1];

		if (querier == null)
		{
			querier = transform;
		}

		generator = new AGridGenerator(querier, enemy, gridSize);

		if (generator != null)
		{
			queryItems = generator.Item();
			//if(queryItems != null)
			//{
			//	for (int x = 0; x < gridSize; x++)
			//	{
			//		for (int z = 0; z < gridSize; z++)
			//		{
			//			isColliding = queryItems[x, z].IsColliding;
			//			if(isColliding)
			//			{
			//				queryItems[x, z].CheckForNeighbor;
			//			}
			//		}
			//	}
			//}
		}
	}

	private void CheckNeighbors(int x, int z)
	{
		if((x + 1) <= gridSize && (!queryItems[x + 1, z].IsColliding && !queryItems[x + 1, z].IsEnemyNearby))
		{
			queryItems[x + 1, z].IsNextToWall = true;
		}

		if ((x - 1) >= 0 && (!queryItems[x - 1, z].IsColliding && !queryItems[x - 1, z].IsEnemyNearby))
		{
			queryItems[x - 1, z].IsNextToWall = true;
		}

		if ((z + 1) <= gridSize && (!queryItems[x, z + 1].IsColliding && !queryItems[x, z + 1].IsEnemyNearby))
		{
			queryItems[x, z + 1].IsNextToWall = true;
		}

		if ((z - 1) >= 0 && (!queryItems[x, z - 1].IsColliding && !queryItems[x, z - 1].IsEnemyNearby))
		{
			queryItems[x, z - 1].IsNextToWall = true;
		}
	}

	private void OnDrawGizmos()
	{
		if (queryItems != null)
		{
			for (int x = 0; x <= gridSize; x++)
			{
				for (int z = 0; z <= gridSize; z++)
				{
					if (queryItems[x, z].IsColliding)
					{
						Gizmos.color = Color.yellow;
						Gizmos.DrawWireSphere(queryItems[x, z].GetWorldLocation(), 0.25f);
						CheckNeighbors(x, z);
					}
					else
					{
						if (queryItems[x, z].IsEnemyNearby)
						{
							Gizmos.color = Color.red;
							Gizmos.DrawWireSphere(queryItems[x, z].GetWorldLocation(), 0.25f);
						}
						else
						{
							if (queryItems[x, z].IsNextToWall)
							{
								Gizmos.color = Color.magenta;
								Gizmos.DrawWireSphere(queryItems[x, z].GetWorldLocation(), 0.25f);
                                hideLocations.Add(queryItems[x, z]);
							}
							else
							{
								Gizmos.color = Color.blue;
								Gizmos.DrawWireSphere(queryItems[x, z].GetWorldLocation(), 0.25f);
							}
						}
					}

					//if (queryItems[x, z].IsColliding)
					//{
					//	Gizmos.color = Color.yellow;
					//	Gizmos.DrawWireSphere(queryItems[x, z].GetWorldLocation(), 0.25f);
					//	//check neighbors and change color
					//	CheckNeighbors(x, z);
					//}
					//else
					//{
					//	if (queryItems[x, z].IsEnemyNearby)
					//	{
					//		Gizmos.color = Color.red;
					//		Gizmos.DrawWireSphere(queryItems[x, z].GetWorldLocation(), 0.25f);
					//	}
					//	else
					//	{
					//		Gizmos.color = Color.blue;
					//		Gizmos.DrawWireSphere(queryItems[x, z].GetWorldLocation(), 0.25f);
					//	}
					//}
				}
			}
		}
	}
}

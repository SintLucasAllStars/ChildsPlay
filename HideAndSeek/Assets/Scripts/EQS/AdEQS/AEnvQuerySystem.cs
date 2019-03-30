using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnvQuerySystem : MonoBehaviour
{
	private IQueryGenerator generator;

	private AEnvQueryItem[,] queryItems;
	public Transform querier;
	public Transform enemy;
	public int gridSize;

	private void Awake()
	{
		queryItems = new AEnvQueryItem[gridSize + 1, gridSize + 1];

		if (querier == null)
		{
			querier = transform;
		}

		generator = new AGridGenerator(querier, enemy, gridSize);

		if (generator != null)
		{
			queryItems = generator.Item();
		}
	}

	private void OnDrawGizmos()
	{
		if(queryItems != null)
		{
			for (int x = 0; x <= gridSize; x++)
			{
				for (int z = 0; z <= gridSize; z++)
				{
					if(queryItems[x,z].IsColliding)
					{
						Gizmos.color = Color.yellow;
						Gizmos.DrawWireSphere(queryItems[x, z].GetWorldLocation(), 0.25f);
					}
					else
					{
						if(queryItems[x,z].IsEnemyNearby)
						{
							Gizmos.color = Color.red;
							Gizmos.DrawWireSphere(queryItems[x, z].GetWorldLocation(), 0.25f);
						}
						else
						{
							Gizmos.color = Color.blue;
							Gizmos.DrawWireSphere(queryItems[x, z].GetWorldLocation(), 0.25f);
						}
					}
				}
			}
		}
	}
}

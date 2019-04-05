using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGridGenerator : IQueryGenerator
{
	private Transform querier;
	private Transform enemy;
	private int gridSize;

	public AGridGenerator(Transform querier, Transform enemy, int gridSize)
	{
		this.querier = querier;
		this.enemy = enemy;
		this.gridSize = gridSize;
	}

	//create and return a two dementional array of EnvQueryItem points
	public AEnvQueryItem[,] Item()
	{
		AEnvQueryItem[,] newItems = new AEnvQueryItem[gridSize + 1, gridSize + 1];

		for (int x = 0; x <= gridSize; x++)
		{
			for (int z = 0; z <= gridSize; z++)
			{
				newItems[x, z] = new AEnvQueryItem(new Vector3(x - 25, 0, z - 25), querier, enemy);
			}
		}
		return newItems;
	}
}

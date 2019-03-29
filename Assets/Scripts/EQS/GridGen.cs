using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGen : IQGenerator
{
	private int GridSize;
	private Transform QuePos; 
	
	public GridGen()
	{
		this.GridSize = 10; 
	}

	public GridGen(int Size, Transform QueryPos)
	{
		this.GridSize = Size;
		this.QuePos = QueryPos; 
	}

	public List<EQSItem> Items(Transform Qposition)
	{
		List<EQSItem> NewItems = new List<EQSItem>();

		for (int x = -GridSize /2; x < GridSize /2; x++)
		{
			for (int z = -GridSize /2; z < GridSize /2; z++)
			{
				NewItems.Add(new EQSItem(new Vector3(x + .5f, 0, z + .5f), Qposition));

			}
		}
		return NewItems;
	}

}

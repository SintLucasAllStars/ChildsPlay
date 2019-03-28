using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGen : MonoBehaviour
{
	int GridSize;
	Transform queruerPos; 
	
	public GridGen()
	{
		this.GridSize = 10; 
	}

	public GridGen(int Size, Transform QYpos)
	{
		this.GridSize = Size;
		this.queruerPos = QYpos; 
	}

	public List<EQSItem> Items(Transform QPosition)
	{
		List<EQSItem> NewItems = new List<EQSItem>();

		for (int x = -GridSize /2; x < GridSize /2; x++)
		{
			for (int z = -GridSize /2; z < GridSize /2; z++)
			{
				NewItems.Add(new EQSItem(new Vector3(x + .5f, 0, z + .5f), queruerPos);

			}
		}
		return NewItems;
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(transform.position, 1f);
		Gizmos.color = Color.yellow; 

	}

}

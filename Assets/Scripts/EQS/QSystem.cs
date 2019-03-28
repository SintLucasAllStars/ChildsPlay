using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QSystem: MonoBehaviour
{
	QGenerator gen;

	List<EQSItem> Qitems;
	public Transform Qr;
	public Transform Enemy;
	public int GridSize;

	public void OnDrawGizmos()
	{
		gen = new GridGen(GridSize, Qr);

		if (gen != null)
		{
			Qitems = gen.Items(transform);
		}

		if (Qitems != null)
		{
			foreach (EQSItem item in Qitems)
			{
				var col = Physics.OverlapSphere(item.GetWorldLocation(), .25f);

				if (col.Length > 0)
				{
					Gizmos.color = Color.yellow;
					Gizmos.DrawWireSphere(item.GetWorldLocation(), 0.25f);
					Gizmos.color = Color.magenta;

				}else
				{

					if (item.RunCheck(enemy))
					{
						Gizmos.color = Color.red;
						Gizmos.DrawWireSphere(item.GetWorldLocation(), 0.25f);

					}
					else
					{
						Gizmos.color = Color.blue;
						Gizmos.DrawWireSphere(item.GetWorldLocation(), 0.25f);

					}
				}

			}
		}
	}

}
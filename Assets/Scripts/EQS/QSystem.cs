using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QSystem : MonoBehaviour
{
	IQGenerator gen;
	public List<EQSItem> Qitems;
	public Transform Qr;
	public Transform target;
	public int GridSize;

	public void Awake()
	{
		gen = new GridGen(GridSize, Qr);

		if (gen != null)
		{

			Qitems = gen.Items(transform);
		}


	}

	public void Update()
	{
		if (Qitems != null)
		{
			foreach (EQSItem item in Qitems)
			{
				var col = Physics.OverlapSphere(item.GetWorldLocation(), .25f);

				if (col.Length > 0)
				{

					item.IsColiding = true;

				}
				else
				{

					if (item.RunCheck(target))
					{
						item.CanHide = false;
					}
					else
					{
						item.CanHide = true;
					}

				}
			}
		}
	}

	public void OnDrawGizmos()
	{
		if (Qitems != null)
		{
			foreach (EQSItem item in Qitems)
			{
				var col = Physics.OverlapSphere(item.GetWorldLocation(), .25f);

				if (col.Length > 0)
				{
					Gizmos.color = Color.yellow;
					Gizmos.DrawWireSphere(item.GetWorldLocation(), 0.25f);


				}
				else
				{

					if (item.RunCheck(target))
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

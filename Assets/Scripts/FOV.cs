using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
	public List<Transform> m_Targets;

	private GameObject hider;

	public float m_ViewRad = 10f;
	public float m_ViewAngle = 30f;

	public bool m_See;

	private void Update()
	{
		foreach (Transform target in m_Targets)
		{
			m_See = false;

			hider = target.gameObject;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < m_ViewAngle / 2)
			{
				if (Physics.Raycast(transform.position, dirToTarget, out RaycastHit hit, m_ViewRad))
				{
					if (hit.collider.name == "Player")
					{
                        Debug.Log("Found player");
						m_See = true; 
					}
				}
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, m_ViewRad);

		float AngleInDegree = (m_ViewAngle / 2);
		Vector3 viewAngleA = new Vector3(Mathf.Sin(AngleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(AngleInDegree * Mathf.Deg2Rad));
		Vector3 viewAngleB = new Vector3(Mathf.Sin(-AngleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(-AngleInDegree * Mathf.Deg2Rad));

		Gizmos.DrawLine(transform.position, transform.position + viewAngleA * m_ViewRad);
		Gizmos.DrawLine(transform.position, transform.position + viewAngleB * m_ViewRad);

		if (m_See)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, hider.transform.position);
		}

	}

}

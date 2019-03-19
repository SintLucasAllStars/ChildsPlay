using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoV : MonoBehaviour
{
	public List<Transform> m_Targets;

	private GameObject hider;

	public float m_ViewRadius = 10f;
	public float m_ViewAngle = 30f;

	private bool m_Seen;

	private void Update()
	{
		foreach (Transform target in m_Targets)
		{
			m_Seen = false;
			Debug.Log("looping through targets");
			hider = target.gameObject;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < m_ViewAngle / 2)
			{
				Debug.Log("In Angle");
				if(Physics.Raycast(transform.position, dirToTarget, out RaycastHit hit, m_ViewRadius))
				{
					Debug.Log("Hit somthing");
					if(hit.collider.name == "Target")
					{
						Debug.Log("Found target");
						m_Seen = true;
					}
				}
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, m_ViewRadius);

		float angleInDegrees = (m_ViewAngle / 2);
		Vector3 viewAngleA = new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
		Vector3 viewAngleB = new Vector3(Mathf.Sin(-angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(-angleInDegrees * Mathf.Deg2Rad));

		Gizmos.DrawLine(transform.position, transform.position + viewAngleA * m_ViewRadius);
		Gizmos.DrawLine(transform.position, transform.position + viewAngleB * m_ViewRadius);

		if(m_Seen)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, hider.transform.position);
		}
	}
}

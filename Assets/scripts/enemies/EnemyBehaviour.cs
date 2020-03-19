using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private enum States
    {
        patrolling,
        searching,
        alert
    }
    private States state = States.patrolling;

    private float sightRange = 50;
    private float fov = 270;

    private NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == States.patrolling)
        {
            seeing();
        }
    }

    private void seeing()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, sightRange);
        foreach (Collider target in targets)
        {
            if (target.CompareTag("Player"))
            {
                GameObject player = target.gameObject;
                float angle = Vector3.Angle(transform.position, player.transform.position);

                if (angle < (fov / 2))
                {
                    Debug.Log(angle < (fov / 2));

                    RaycastHit hit;

                    Vector3 dir = player.transform.position - transform.position;
                    if (Physics.Raycast(transform.position, dir, out hit))
                    {
                        navAgent.SetDestination(hit.point);
                    }
                }
            }
        }
    }
}

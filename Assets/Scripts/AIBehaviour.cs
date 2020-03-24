using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    public NavMeshAgent navMesh;
    public GameObject destination;

    public float chaseSpeed = 4;

    public float FOV;
    public float range = 5;
    public float angleToTarget;

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = destination.transform.position - transform.position;
        angleToTarget = Vector3.Angle (p, transform.forward);
        
        if (angleToTarget <= FOV / 2.0f)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, p, out hit, range))
            {
                Debug.DrawRay(transform.position, p, Color.red);

                if (hit.collider.gameObject == destination)
                {
                        navMesh.speed = chaseSpeed;
                        navMesh.destination = destination.transform.position;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    #region declaration
    private enum States
    {
        patrolling,
        searching,
        alert
    }
    private States state = States.patrolling;

    public float sightRange;
    public float fov;

    private NavMeshAgent navAgent;

    private Transform pathMarkerGroup;
    private Transform[] pathMarkers;
    private int currentPathIndex;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region assigning nav agent
        navAgent = GetComponent<NavMeshAgent>();
        #endregion

        #region path assigning
        pathMarkerGroup = transform.parent.GetChild(1);
        pathMarkers = new Transform[pathMarkerGroup.childCount];

        for (int i = 0; i < pathMarkerGroup.childCount; i++)
        {
            pathMarkers[i] = pathMarkerGroup.GetChild(i);
        }

        //setting position
        navAgent.destination = pathMarkers[currentPathIndex].position;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region disition making
        //work based on enemy state
        if (state == States.patrolling)
        {
            if (!seeing())
            {
                Patrolling();
            }
        }
        #endregion
    }

    /// <summary>
    /// patrol between preset points
    /// </summary>
    private void Patrolling()
    {
        //check if position has been reach
        if(Vector3.Distance(transform.position, pathMarkers[currentPathIndex].position) < 3)
        {
            //increase position
            currentPathIndex++;

            //reset when at the end
            if (currentPathIndex > pathMarkers.Length - 1)
            {
                currentPathIndex = 0;
            }

            //setting new position
            navAgent.destination = pathMarkers[currentPathIndex].position;
        }
    }

    /// <summary>
    /// returns bool if player is within sight and chase the player when seen
    /// </summary>
    /// <returns></returns>
    private bool seeing()
    {
        //creating return value
        bool value = false;

        //searching all colliders in range
        Collider[] targets = Physics.OverlapSphere(transform.position, sightRange);

        //looping through all colliders
        foreach (Collider target in targets)
        {
            //checking if collider is attacht to player
            if (target.CompareTag("Player"))
            {
                //setting local variables
                GameObject player = target.gameObject;
                Vector3 dir = player.transform.position - transform.position;
                float angle = Vector3.Angle(dir, transform.forward);

                //checking if player is within Field Of View
                if (angle <= (fov / 2))
                {
                    RaycastHit hit;
                    //checking if their are any objects between player and enemy
                    if (Physics.Raycast(transform.position, dir, out hit))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            //setting new search position
                            navAgent.SetDestination(hit.point);
                            //setting value
                            value = true;
                        }
                    }
                }
            }
        }
        return value;
    }
}

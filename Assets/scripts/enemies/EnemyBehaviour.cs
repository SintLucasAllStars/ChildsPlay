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

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region assigning nav agent
        navAgent = GetComponent<NavMeshAgent>();
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
                //TODO: add patrolling AI
            }
        }
        #endregion
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
                    Debug.DrawRay(transform.position, dir);
                    //checking if their are any objects between player and enemy
                    if (Physics.Raycast(transform.position, dir, out hit))
                    {
                        //setting new search position
                        navAgent.SetDestination(hit.point);
                        //setting value
                        value = true;
                    }
                }
            }
        }
        return value;
    }
}

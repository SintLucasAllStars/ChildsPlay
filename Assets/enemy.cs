using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    bool close;
    [SerializeField]
    Transform destination;
    public Transform player;
    float distance;

    NavMeshAgent navMeshAgent;

    // Use this for initialization
    void Start()
    {

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        close = false;

    }


    private void Update()
    {
        distance =Vector3.Distance(transform.position,destination.position);
        if (distance <= 10)
        {
            close = true;
        }
        else
        {
            close = false;
        }
        if (close == true)
        {

            if (navMeshAgent == null)
            {
                //Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
            }
            else
            {
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }
}
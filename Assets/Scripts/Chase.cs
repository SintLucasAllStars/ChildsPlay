using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    bool tagged = false;                                    //Vincent

    [SerializeField]
    Transform destination;                                    //Wessel

    NavMeshAgent navMeshAgent;                                //Wessel 

    // Use this for initialization
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();   //Wessel
    }

    private void Update()                                    //Wessel
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

    private void SetDestination()                                        //We both worked on this code, I made the first part and Vincent added the second.
    {
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            if (tagged == true)
            {
                navMeshAgent.SetDestination(-targetVector);
            }
            else
            {
                navMeshAgent.SetDestination(targetVector);
            }
        }

    }

    void OnCollisionEnter(Collision collision)                            //Vincent
    {
        if (collision.gameObject.tag == "player")
        {
            tagged = !tagged;
        }
    }
}
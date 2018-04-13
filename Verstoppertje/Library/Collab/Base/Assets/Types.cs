using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Types : MonoBehaviour
{

    NavMeshAgent navMeshAgent;
    public enum Type
    {
        Tikker,
        NietTikker
    }
    public Type type;
    public bool canWalk = true;
    Vector3 randomPos;

    void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

	void Update () {
        Walking();
	}

    void Walking()
    {
        if (canWalk)
        {
            if (navMeshAgent.remainingDistance < 1f)
            {
                randomPos = new Vector3(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100));

                navMeshAgent.SetDestination(randomPos);
            }
        }
    }
}

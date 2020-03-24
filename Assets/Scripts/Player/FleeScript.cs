using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeScript : MonoBehaviour
{
    private NavMeshAgent _agent;

    public Transform Player;

    public float EnemyDistanceRun = 5f;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        Debug.Log("Distance" + distance);

        if (distance < EnemyDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;

            _agent.SetDestination(newPos);
        }
        
        
    }
    
    
}

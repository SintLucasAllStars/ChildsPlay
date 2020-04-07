using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

//needed to word with the AI components

public class EnemyBehaviour : MonoBehaviour
{
    NavMeshAgent nav; //NavMeshAgent (nav) on enemy

    public Transform target;
    
    enum EnemyStates{Flee,Chase}
    EnemyStates enemy;

    private bool isChasing = false;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        //switch
        switch (enemy)
        {
            case EnemyStates.Flee:
                FleeEnemy();
                isChasing = false;
                break;
            case EnemyStates.Chase:
                ChaseEnemy();
                isChasing = true;
                break;
            default: 
                break;
                    
        }
        Debug.Log(enemy);
    }

   private void OnCollisionEnter(Collision other)
    {
     
        if (other.gameObject.CompareTag("Player") && isChasing == true)
        {
            enemy = EnemyStates.Flee;
        }
        if (other.gameObject.CompareTag("Player") && isChasing == false)
        {
            enemy = EnemyStates.Chase;
        }
    }

    /*private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && enemy == EnemyStates.Chase)
        {
            enemy = EnemyStates.Flee;
        }
        if (other.gameObject.CompareTag("Player") && enemy == EnemyStates.Flee)
        {
            enemy = EnemyStates.Chase;
        }
    }
*/

    private void ChaseEnemy()
    {
        nav.destination = target.position; 
    }

    void FleeEnemy()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < 10)
        {
            Vector3 dirToPlayer = transform.position - target.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;

            nav.SetDestination(newPos);
        }

    }
}

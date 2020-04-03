using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public enum States
    {
        Patrol, Chase
    }

    public Transform target;
    NavMeshAgent nvmesh;
    public float fov = 60; 
    public float range = 3;
    public States currentState = States.Patrol;
    public bool gameStarted = false;

    void Start()
    {
        Debug.Log(target.position);
        nvmesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            gameStarted = true;
        }

        if (gameStarted)
        {
            if (SeesTarget())
            {
                currentState = States.Chase;
            }
            else
            {
                currentState = States.Patrol;
            }

            switch (currentState)
            {
                case States.Patrol:
                    if (Time.frameCount % 30 == 0)
                    {
                        Vector3 newDest = transform.position + new Vector3(Random.Range(-1f, 1f), transform.position.y, Random.Range(-1f, 1f)) * 6;
                        nvmesh.destination = newDest;
                    }
                    break;
                case States.Chase:
                    nvmesh.destination = target.position;
                    break;
            }
        }
    }

    public bool SeesTarget()
        {
            Vector3 p = target.position - transform.position;
            float angle = Vector3.Angle(p, transform.forward);
            if (angle <= fov / 2.0f)
            {
                RaycastHit hit;

                if (Physics.Raycast(origin: transform.position, direction: p, out hit, range))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
}

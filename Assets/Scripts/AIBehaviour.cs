using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    enum State { idle, chase, lookaround };
    State currentState;

    public NavMeshAgent navMesh;
    public GameObject destination;
    public Vector3 idleDestination;

    public float idleSpeed = 2;
    public float chaseSpeed = 4;

    public float FOV;
    public float range = 5;
    public float angleToTarget;
    Vector3 p;

    public float waitTime = 3; 

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        p = destination.transform.position - transform.position;
        angleToTarget = Vector3.Angle (p, transform.forward);
        
        switch (currentState)
        {
            case State.idle:
                StartCoroutine(SetRandomTarget(waitTime));
                navMesh.speed = idleSpeed;
                navMesh.destination = idleDestination;
                break;

            case State.lookaround:
                for (int i = 0; i < 4; i++)
                {
                    StartCoroutine(SetRandomTarget(waitTime / 2));
                    navMesh.speed = 0.05f;
                    navMesh.destination = idleDestination;
                }
                currentState = State.idle;
                break;

            case State.chase:
                navMesh.speed = chaseSpeed;
                navMesh.destination = destination.transform.position;
                break;
                
            default:
                break;
        }

        if (angleToTarget <= FOV / 2.0f)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, p, out hit, range))
            {
                Debug.DrawRay(transform.position, p, Color.red);

                if (hit.collider.gameObject == destination)
                {
                    currentState = State.chase;
                }
                else
                {
                    currentState = State.lookaround;
                }
            }
        }
    }

    IEnumerator SetRandomTarget(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        idleDestination = transform.position + new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y, transform.position.z + Random.Range(-2.5f, 2.5f));
    }

    IEnumerator SetLookAroundState(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        RaycastHit hit;

        if (!(Physics.Raycast(transform.position, p, out hit, range)))
        {
            currentState = State.lookaround;
        }
    }
}

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
    playerBehaviour playerBehaviour;
    public Vector3 idleDestination;

    public float idleSpeed = 1.5f;
    public float chaseSpeed = 3;

    public float FOV;
    public float range = 5;
    public float angleToTarget;
    Vector3 p;

    public float waitTime = 3;

    bool randomPositionResetter;

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        playerBehaviour = destination.GetComponent<playerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the viariable p and the angleToTarget
        p = destination.transform.position - transform.position;
        angleToTarget = Vector3.Angle (p, transform.forward);

        // If the player is hidden, set the state to lookaround
        if (playerBehaviour.isHidden)
        {
            StartCoroutine(SetLookAroundState(0.5f));
        }
        
        // A switch case for all the different states the AI can be in
        switch (currentState)
        {
            // The AI slowly walks around to random points
            case State.idle:
                // If the PosResetter is not true, call the Coroutine
                if (!randomPositionResetter)
                    StartCoroutine(SetRandomTarget(waitTime));
                randomPositionResetter = true;
                navMesh.speed = idleSpeed;
                navMesh.destination = idleDestination;
                break;

            // The AI looks around toward random points and barely moving
            // This case is usually active after the AI loses the target
            case State.lookaround:
                if (!randomPositionResetter)
                    StartCoroutine(SetRandomLookTarget(waitTime));

                break;

            // The AI chases after the target
            case State.chase:
                navMesh.speed = chaseSpeed;
                navMesh.destination = destination.transform.position;
                break;
                
            default:
                break;
        }

        // If the angle to the target is in the range of sight
        if (angleToTarget <= FOV / 2.0f)
        {
            RaycastHit hit;

            // Shoots a raycast out toward the target, it's as long as its range is
            if (Physics.Raycast(transform.position, p, out hit, range))
            {
                Debug.DrawRay(transform.position, p, Color.red);

                // If the raycast hits the target, give chase
                if (hit.collider.gameObject == destination)
                {
                    currentState = State.chase;
                }
                // If the raycast does not hit the target, start looking around
                else
                {
                    StartCoroutine(SetLookAroundState(waitTime));
                }
            }
        }
    }

    // This Coroutine sets a random target position around the AI
    IEnumerator SetRandomTarget(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Debug.Log("RandomTarget");

        idleDestination = this.transform.position + new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y, transform.position.z + Random.Range(-2.5f, 2.5f));

        randomPositionResetter = false;
    }

    // This Coroutine sets a random target position for the looking around the AI
    IEnumerator SetRandomLookTarget(float waitTime)
    {
        // Set the point resetter to true so that the Coroutine isn't called every frame from the switch case
        randomPositionResetter = true;
        navMesh.speed = 0.05f;

        for (int i = 0; i < 4; i++)
        {
            Debug.Log("RandomLookTarget");

            idleDestination = this.transform.position + new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y, transform.position.z + Random.Range(-2.5f, 2.5f));
            navMesh.destination = idleDestination;
            yield return new WaitForSeconds(waitTime);
        }

        currentState = State.idle;
        randomPositionResetter = false;
    }

    // This Coroutine sets the AI to the lookaround state after waitTime if the raycast does not hit the target
    IEnumerator SetLookAroundState(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        currentState = State.lookaround;
    }

    // If the ai touches the player, the game is over
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("GameOver!");
        }
    }
}

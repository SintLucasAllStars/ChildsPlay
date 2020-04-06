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

    public float hearingRange;
    public float sightRange;
    public float fov;
    private float detection;
    private float timer;

    private NavMeshAgent navAgent;

    #region patrolling

    [Header("walk back")]
    [Tooltip("set to true, the enemy will walk back the path oppone completen")]
    public bool backTrackPath;
    [Space(20)]
    private int pathModifier = 1;

    private Transform pathMarkerGroup;
    private Transform[] pathMarkers;
    private int currentPathIndex;
    #endregion

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
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (detection > 0 || state != States.alert)
        {
            detection -= Time.deltaTime;
        }
        else if (detection < 0 || state != States.patrolling)
        {
            state = States.patrolling;
        }

        #region disition making
        //work based on enemy state
        if (state == States.patrolling)
        {
            if (!seeing() && !Hearing() && detection <= 0)
            {
                Patrolling();
            }
        }
        else if (state == States.searching)
        {
            seeing();
            Hearing();
            WalkAround();
        }
        else if (state == States.alert)
        {
            if (!seeing() && detection > 0)
            {
                state = States.searching;
            }
        }
        #endregion
    }

    #region disitions
    /// <summary>
    /// patrol between preset points
    /// </summary>
    private void Patrolling()
    {
        //setting position
        if (navAgent.destination != pathMarkers[currentPathIndex].position)
        {
            navAgent.destination = pathMarkers[currentPathIndex].position;
        }

        //check if position has been reach
        if (Vector3.Distance(transform.position, pathMarkers[currentPathIndex].position) < 2)
        {
            //reset when at the end
            if (currentPathIndex + 1 > pathMarkers.Length - 1)
            {
                if (backTrackPath)
                {
                    pathModifier = -pathModifier;
                }
                else
                {
                    currentPathIndex = 0;
                }
            }

            //increase position
            currentPathIndex += pathModifier;

            //setting new position
            navAgent.destination = pathMarkers[currentPathIndex].position;
        }
    }

    private void WalkAround()
    {
        if (timer <= 0)
        {
            navAgent.destination = new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y, transform.position.z + Random.Range(-5, 5));
            timer = 3;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    #endregion


    #region senses
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

                RaycastHit hit;
                //checking if player is within Field Of View
                if (angle <= (fov / 2))
                {
                    //checking if their are any objects between player and enemy
                    if (Physics.Raycast(transform.position, dir, out hit))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            if (detection > 100)
                            {
                                detection = 100;
                                continue;
                            }
                            else if (detection > 50)
                            {
                                //setting new search position
                                navAgent.SetDestination(hit.point);
                                state = States.alert;
                                //setting value
                                value = true;
                            }
                            else
                            {
                                //setting search time
                                detection += 5 * Time.deltaTime;
                            }
                        }
                    }
                }
                else if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
                {
                    //checking if their are any objects between player and enemy
                    if (Physics.Raycast(transform.position, dir, out hit))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            if (detection > 100)
                            {
                                detection = 100;
                                continue;
                            }
                            else if (detection > 50)
                            {
                                //setting new search position
                                navAgent.SetDestination(hit.point);
                                state = States.alert;
                                //setting value
                                value = true;
                            }
                            else
                            {
                                //setting search time
                                detection += 2.5f * Time.deltaTime;
                                print(detection);
                            }
                        }
                    }
                }
            }
        }
        return value;
    }

    private bool Hearing()
    {
        bool value = false;

        //searching all colliders in range
        Collider[] targets = Physics.OverlapSphere(transform.position, sightRange);

        //looping through all colliders
        foreach (Collider target in targets)
        {
            if (target.CompareTag("SoundPing"))
            {
                NavMeshPath path = new NavMeshPath();
                NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path);
                float distance = 0;

                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    if (i > 0)
                    {
                        distance += Vector3.Distance(transform.position, path.corners[i]);
                    }
                    else
                    {
                        distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
                    }
                }

                if (distance < hearingRange / 2)
                {
                    detection = 50;

                    navAgent.destination = target.transform.position;

                    value = true;
                }
            }
        }

        return value;
    }
    #endregion
}

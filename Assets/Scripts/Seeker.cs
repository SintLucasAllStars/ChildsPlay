using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seeker : MonoBehaviour
{

    public enum Mode { Patrol, Search, Chase };
    public Mode mode;

    public GameObject[] testing = new GameObject[7];

   // public List<GameObject> hiders = new List<GameObject>();

    public Transform target;

    public float patrolSpeed;
    public float maxSpeed;
    public float detectionRate;

    bool canSee;

    float fov = 120f;

    NavMeshAgent nav;

    Vector3 lastSeen;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        //hiders.Add(GameObject.FindGameObjectsWithTag)("Hider").transform);

        testing = GameObject.FindGameObjectsWithTag("Hider");

       /* foreach (GameObject hider in GameObject.FindGameObjectsWithTag("Hider"))
        {
            hiders.Add(hider);
        }*/
        //hiders.Add(GameObject.FindGameObjectsWithTag("Hider"));
        target = GameObject.FindGameObjectWithTag("Hider").transform;

        patrolSpeed = nav.speed;
        maxSpeed = patrolSpeed * 2;

        SetMode(Mode.Patrol);

        StartCoroutine(PatrolBehaviour());
    }

    void Update()
    {
        Debug.Log(mode);
        Eyes();
        //bool canSee = CanSeeTarget();
        if (canSee == false && mode != Mode.Chase && detectionRate > 1)
        {
            detectionRate = detectionRate - 10 * Time.deltaTime;
        }
        if (canSee && mode != Mode.Chase)
        {
            detectionRate = detectionRate + 50 * Time.deltaTime;
            if (detectionRate > 99)
            {
                SetMode(Mode.Chase);
            }
        }

        switch (mode)
        {
            case Mode.Patrol:
                break;
            case Mode.Search:
                if (nav.remainingDistance < .5f)
                {
                    SetMode(Mode.Patrol);
                }
                break;
            case Mode.Chase:
                nav.speed = maxSpeed;
                if (canSee)
                {
                    nav.SetDestination(target.position);
                }
                else
                {
                    SetMode(Mode.Search);
                }
                break;
        }
    }

    IEnumerator PatrolBehaviour()
    {
        while (true)
        {
            if (mode == Mode.Patrol)
            {
                Vector3 destination = transform.position + new Vector3(Random.Range(-10, 10), 0f, Random.Range(-10, 10));
                nav.SetDestination(destination);
            }
            yield return new WaitForSeconds(Random.Range(3, 7));
        }
    }

    void SetMode(Mode m)
    {
        mode = m;
        switch (mode)
        {
            case Mode.Patrol:
                nav.speed = patrolSpeed;
                break;
            case Mode.Search:
                nav.SetDestination(lastSeen);
                break;
            case Mode.Chase:
                nav.speed = maxSpeed;
                lastSeen = target.position;
                break;
        }
    }

    public void Alert()
    {
        if (mode != Mode.Chase)
        {
            SetMode(Mode.Chase);
        }
    }

    /*bool CanSeeTarget()
    {
        RaycastHit hit;
        
        Vector3 direction = target.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Hider"))
            {
                float angle = Vector3.Angle(transform.forward, direction);
                if (angle < fov / 2)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debug.Log("I See nothing");
            return false;
        }       
    }*/

    public void Eyes()
    {
        RaycastHit hit;
        for (int i = 0; i < testing.Length; i++)
        {
            Debug.DrawRay(transform.position, testing[i].transform.position - transform.position);

            if (Physics.Raycast(transform.position, (testing[i].transform.position - transform.position), out hit))
            {
                if (hit.collider.gameObject.CompareTag("Hider"))
                {
                    float angle = Vector3.Angle(transform.forward, (testing[i].transform.position - transform.position));
                    if (angle < fov / 2)
                    {
                        target = testing[i].transform;
                        canSee = true;
                    }
                    else
                    {
                        canSee = false;
                    }
                }
                else
                {
                    canSee = false;
                }
            }
            else
            {
                Debug.Log("I See nothing");
                canSee = false;
            }
        }/*
        foreach (GameObject hiders in hiders)
        {
            Debug.DrawRay(transform.position, hiders.transform.position - transform.position);

            if (Physics.Raycast(transform.position, (hiders.transform.position - transform.position), out hit))
            {
                if (hit.collider.gameObject.CompareTag("Hider"))
                {
                    float angle = Vector3.Angle(transform.forward, (hiders.transform.position - transform.position));
                    if (angle < fov / 2)
                    {
                        target = hiders.transform;
                        canSee = true;
                    }
                    else
                    {
                        canSee = false;
                    }
                }
                else
                {
                    canSee = false;
                }
            }
            else
            {
                Debug.Log("I See nothing");
                canSee = false;
            }
        }*/
    }
}
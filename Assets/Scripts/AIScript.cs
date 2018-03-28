using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour {
    
    public enum Mode { Patrol, Search, Chase, Suspicious}
    private NavMeshAgent nav;
    public Transform target;
    Vector3 lastSeen;

    public Mode mode;
    public float maxSpeed;
    public float patrolSpeed;

    public float fov = 120;
	// Use this for initialization
	void Start () {
        nav = GetComponent<NavMeshAgent>();
        patrolSpeed = nav.speed;
        maxSpeed = patrolSpeed * 2;

        target = GameObject.FindGameObjectWithTag("Player").transform;
        SetMode(Mode.Patrol);
        StartCoroutine("PatrolBehaviour");
        
	}
	
	// Update is called once per frame
	void Update () {
        bool canSee = CanSeeTarget();

        if (canSee && mode != Mode.Chase)
        {
            SetMode(Mode.Chase);
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
                if (CanSeeTarget())
                {
                    nav.SetDestination(target.position);
                }
                else
                {
                    SetMode(Mode.Search);
                }
                break;
            case Mode.Suspicious:
                break;
        }

    }

    IEnumerator PatrolBehaviour()
    {
        while (true)
        {
            if (mode == Mode.Patrol)
            {
                Vector3 destination = new Vector3(Random.Range(-50, 51), 0f, Random.Range(-50, 51));
                Debug.Log(destination);
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

    public bool CanSeeTarget()
    {
        RaycastHit hit;

        Vector3 direction = target.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
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

    }
}

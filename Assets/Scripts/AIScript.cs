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
    float distance;

    public float fov = 120;
    public float reactionTime;
    public int reactionTimer = 0;
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
        distance = Vector3.Distance(target.position, transform.position);

        if (distance < 10)
        {
            if(ThirdPersonScript.conspicuousness == 1)
            {
                mode = Mode.Suspicious;
            }

            if(ThirdPersonScript.conspicuousness == 0.5f)
            {
                mode = Mode.Patrol;
            }

            if(ThirdPersonScript.conspicuousness == 2)
            {
                mode = Mode.Chase;
                transform.LookAt(target.transform);
            }
        }
        bool canSee = CanSeeTarget();

        if (canSee && mode == Mode.Patrol)
        {
            SetMode(Mode.Suspicious);
        }
        else
        {
            if(mode != Mode.Suspicious)
            {
                reactionTimer = 0;
            }
        }

        if (!canSee)
        {
            if(mode == Mode.Suspicious)
            {
                SetMode(Mode.Patrol);
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
                reactionTimer++;
                if (reactionTimer > reactionTime)
                {
                    SetMode(Mode.Chase);
                    reactionTimer = 0;
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
                //Vector3 destination = new Vector3(Random.Range(-50, 51), 0f, Random.Range(-50, 51));
                //Debug.Log(destination);
                //nav.SetDestination(destination);
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
                nav.speed = patrolSpeed;
                nav.SetDestination(lastSeen);
                break;
            case Mode.Chase:
                nav.speed = maxSpeed;
                lastSeen = target.position;
                break;
            case Mode.Suspicious:
                nav.speed = 0;
                transform.LookAt(lastSeen);
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

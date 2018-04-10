using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour {
	public enum Mode {Patrol, Search, Chase, Panic,Run,Hide};

    public GameManager theManager;
	NavMeshAgent nav;
	float fov = 120f;
	Vector3 currentHidingSpot;
    public Quaternion hideSpotRotation;
	public Transform chaser;
	public new Light light;
	public Mode mode;
	public float speed;
    public float Sound;
    public Transform[] hidingspots;
    private bool gameStarted= false;

    // Use this for initialization
    private void Awake()
    {
        mode = Mode.Chase;
        nav = GetComponent<NavMeshAgent>();

    }

    void Start ()
    {
        SetMode(Mode.Run);

        theManager = GameObject.Find("Managers").GetComponent<GameManager>();


        chaser = GameObject.FindGameObjectWithTag("Player").transform;
       
        //StartCoroutine(AiBehaviour());
    }
	
	// Update is called once per frame
	void Update ()
	{
	    IfPlayerVisible();

        if (mode == Mode.Hide)
        {
            nav.destination = transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, hideSpotRotation, 1 * Time.deltaTime);
        }

	    
	    
	        if (nav.remainingDistance <= 5f &&  nav.remainingDistance >=2)
	        {
	            SetMode(Mode.Hide);
	        }
	    
    }

    IEnumerator Hiding()
    {
        while (true)
        {
            nav.updatePosition = false;
            nav.updateRotation = false;
            hideSpotRotation = Quaternion.LookRotation(new Vector3(Random.Range(-360, 360), 0, 0) - transform.position);
            Debug.Log("test");
            yield return new WaitForSeconds(3);
        }

    }

    void IfPlayerVisible()
    {
        bool canSee = canSeeChaser();
        
        // happens every frame 
        if (canSee && mode == Mode.Hide)
        {
            SetMode(Mode.Run);
            Debug.Log("you got here");

        }

        
        if (canSee && mode == Mode.Run)
        {

            SetMode(Mode.Panic);
            Debug.Log("Panic");



        }


        
            
        
    }

 

    IEnumerator AiBehaviour()
	{
		while(true)
		{
			if(mode == Mode.Patrol)
			{
				//Vector3 destination = transform.position + new Vector3(Random.Range(-10, 10), 0f, Random.Range(-10, 10));
				//nav.SetDestination(destination);
			}
			yield return new WaitForSeconds(Random.Range(3,7));
		}
	}


   

    // when you change mode
	public void SetMode(Mode m)
	{
		mode = m;
		switch (mode)
		{
            case Mode.Run:
                StopCoroutine("Hiding");
                nav.destination = hidingspots[Random.Range(0, hidingspots.Length)].transform.position;
                light.color = Color.yellow;
                nav.updatePosition = true;
                nav.updateRotation = true;
                nav.speed = 20;
                break;

            case Mode.Panic:
                light.color = Color.red;

                nav.speed = 50;
                break;
            case Mode.Hide:
                StartCoroutine("Hiding");
                light.color = Color.blue;
                break;
		case Mode.Patrol:
		
			break;
		case Mode.Search:
			break;
		case Mode.Chase:
			break;
		}
	}


    public void StupidWorkAround()
    {
        SetMode(Mode.Run);
    }

    bool canSeeChaser()
	{
		RaycastHit hit;

		Vector3 direction = chaser.position - transform.position;
		if(Physics.Raycast(transform.position, direction, out hit))
		{
			if(hit.collider.gameObject.CompareTag("Player"))
			{
				float angle = Vector3.Angle(transform.forward, direction);
				if(angle < fov/2)
				{
					return true;
				}
				else
				{
					return false;
				}
			} else 
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

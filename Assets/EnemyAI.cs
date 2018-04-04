using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
	public enum Mode {Patrol, Search, Chase, Panic,Run,Hide};

	NavMeshAgent nav;
	float fov = 120f;
	Vector3 lastSeen;

	public Transform chaser;
	public Light light;
	public Mode mode;
	public float speed;
    public float Sound;
    public Transform[] hidingspots;

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent>();
	   
        chaser = GameObject.FindGameObjectWithTag("Player").transform;
		SetMode(Mode.Patrol);
		//StartCoroutine(AiBehaviour());
	}
	
	// Update is called once per frame
	void Update () {
		bool canSee = canSeeChaser();

		if(canSee && mode != Mode.Panic)
		{
		  
		    
		        SetMode(Mode.Panic);

            
        }


        // happens every frame 
	    switch (mode)
	    {

            case Mode.Run:
                // a sound comes here
                break;
	        case Mode.Panic:
                // a extra hard sound
	            break;
	        case Mode.Hide:
	            if (canSee == true && mode !=Mode.Panic)
	            {
	                
                    SetMode(Mode.Run);
	            }

                break;
        }

	}

	IEnumerator AiBehaviour()
	{
		while(true)
		{
			if(mode == Mode.Patrol)
			{
				Vector3 destination = transform.position + new Vector3(Random.Range(-10, 10), 0f, Random.Range(-10, 10));
				nav.SetDestination(destination);
			}
			yield return new WaitForSeconds(Random.Range(3,7));
		}
	}



    // when you change mode
	void SetMode(Mode m)
	{
		mode = m;
		switch (mode)
		{
            case Mode.Run:

                break;

            case Mode.Panic:

                break;
            case Mode.Hide:

                break;
		case Mode.Patrol:
		
			light.color = Color.blue;
			break;
		case Mode.Search:
			nav.SetDestination(lastSeen);
			light.color = Color.yellow;
			break;
		case Mode.Chase:
			light.color = Color.red;
			lastSeen = chaser.position;
			CentralIntellignece.instance.Alert(gameObject);
			break;
		}
	}

	//public void Alert(){
	//	if(mode != Mode.Chase)
	//	{
	//		SetMode(Mode.Chase);
	//	}
	//}

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

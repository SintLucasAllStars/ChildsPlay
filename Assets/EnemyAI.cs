using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
	public enum Mode {Patrol, Search, Chase, Panic,Run,Hide};

	NavMeshAgent nav;
	float fov = 120f;
	Vector3 currentHidingSpot;

	public Transform chaser;
	public Light light;
	public Mode mode;
	public float speed;
    public float Sound;
    public GameObject hideSpots;
    public Transform[] hidingspots;

	// Use this for initialization
    private void Awake()
    {


    }

    void Start () {

        nav.destination = hidingspots[Random.Range(0, hidingspots.Length)].transform.position;

        SetMode(Mode.Run);

        chaser = GameObject.FindGameObjectWithTag("Player").transform;
		//StartCoroutine(AiBehaviour());
	}
	
	// Update is called once per frame
	void Update () {
		bool canSee = canSeeChaser();

		if(canSee && mode != Mode.Panic)
		{
		  Debug.Log("teste");
		    
		        SetMode(Mode.Panic);

            
        }

	  

        // happens every frame 
	    if (canSee&& mode ==Mode.Hide)
	    {
	        SetMode(Mode.Run);
	    }

	    if (canSee == false)
	    {
	        SetMode(Mode.Hide);
        }
      
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("HideSpot"))
        {
            

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
	void SetMode(Mode m)
	{
		mode = m;
		switch (mode)
		{
            case Mode.Run:
                light.color = Color.yellow;

                nav.speed = 20;

                nav.destination = hidingspots[Random.Range(0,hidingspots.Length)].transform.position;
                break;

            case Mode.Panic:
                light.color = Color.red;

                nav.speed = 50;
                break;
            case Mode.Hide:
                nav.destination = transform.position;
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

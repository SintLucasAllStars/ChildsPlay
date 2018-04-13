using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum Mode
    {
        Patrol,
        Search,
        Chase,
        Panic,
        Run,
        Hide
    }

    public Transform chaser;
    private Vector3 currentHidingSpot;
    private readonly float fov = 120f;
    private bool gameStarted = false;
    public Quaternion hideSpotRotation;
    public Transform[] hidingspots;
    public new Light light;
    public Mode mode;
    private NavMeshAgent nav;
    public float Sound;
    public float speed;

    public GameManager theManager;

    // Use this for initialization
    private void Awake()
    {
        mode = Mode.Chase;
        nav = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetMode(Mode.Run);

        theManager = GameObject.Find("Managers").GetComponent<GameManager>();


        chaser = GameObject.FindGameObjectWithTag("Player").transform;

        //StartCoroutine(AiBehaviour());
    }

    // Update is called once per frame
    private void Update()
    {
        IfPlayerVisible();

        if (mode == Mode.Hide)
        {
            nav.destination = transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, hideSpotRotation, 1 * Time.deltaTime);
        }


        if (nav.remainingDistance <= 8f && nav.remainingDistance >= 2) SetMode(Mode.Hide);
    }

    private IEnumerator Hiding()
    {
        while (true)
        {
            nav.updatePosition = false;
            nav.updateRotation = false;
            hideSpotRotation = Quaternion.LookRotation(new Vector3(Random.Range(-360, 360), 0, 0) - transform.position);
            yield return new WaitForSeconds(3);
        }
    }

    private void IfPlayerVisible()
    {
        var canSee = canSeeChaser();

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


    public void HearingLoudNoise()
    {
        // run for selecting hidespot and panic to quickly run away
        SetMode(Mode.Run);
        SetMode(Mode.Panic);

        // this is for preventing the hiding spot to be the one he is on now.
        if (nav.destination == transform.position) HearingLoudNoise();
    }

    public void HearingNormalNoise()
    {
        SetMode(Mode.Run);
        // this is for preventing the hiding spot to be the one he is on now.
        if (nav.destination == transform.position) HearingNormalNoise();
    }

    private bool canSeeChaser()
    {
        RaycastHit hit;

        var direction = chaser.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit))
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                var angle = Vector3.Angle(transform.forward, direction);
                if (angle < fov / 2)
                    return true;
                return false;
            }
            else
            {
                return false;
            }

        Debug.Log("I See nothing");
        return false;
    }

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Bullet")
		{
		    theManager.hiders.Remove(this.gameObject);
		    theManager.hiderAis.Remove(this.GetComponent<EnemyAI>());
			Destroy (this.gameObject);
		}
	}
}
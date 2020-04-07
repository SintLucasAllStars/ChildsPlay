using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public enum States { Uninfected, Flee, Infected, Deceased }
    public States currentState = States.Uninfected;

    private NavMeshAgent nav;
    public Transform target;
    public Transform patient0;

    public Material Uninfected;
    public Material Infected;

    public float timeAlive;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.destination = new Vector3(Random.Range(11, 40), transform.position.y, Random.Range(11, 40));
        nav.speed = 2.5f;
        timeAlive = 10;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        float range = 4.5f;

        if (Physics.Raycast(transform.position, patient0.position - transform.position, out hit, range))
        {
            if (hit.collider.gameObject.CompareTag("Player") && currentState != States.Infected)
            {
                currentState = States.Flee;
            }
            else if (!hit.collider.gameObject.CompareTag("Player") && currentState != States.Infected)
            {
                currentState = States.Uninfected;
            }
            Debug.DrawRay(transform.position, patient0.position - transform.position, Color.red, 0.1f);
        }

        switch (currentState)
        {
            case States.Uninfected:
                if (Time.frameCount % 240 == 0)
                {
                    nav.destination = new Vector3(Random.Range(11, 40), transform.position.y, Random.Range(11,40));
                }
                break;

            case States.Flee:
                flee();
                break;

            case States.Infected:
                nav.destination = target.position;
                timeAlive -= Time.deltaTime;
                if (timeAlive <= 0)
                {
                    currentState = States.Deceased;
                }
                break;

            case States.Deceased:
                Destroy(this.gameObject);
                break;

            default:
                goto case States.Uninfected;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Infected"))
        {
            nav.speed = 2;
            gameObject.tag = "Infected";
            currentState = States.Infected;
            GetComponent<Renderer>().material = Infected;
        }
    }

    private void flee()
    {
        Transform startTransform = transform;

        transform.rotation = Quaternion.LookRotation(transform.position - patient0.position);

        Vector3 runTo = transform.position + transform.forward * Random.Range(1f,2f);
        NavMeshHit hit;

        // ???
        NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Default"));

        nav.SetDestination(hit.position);
    }
}

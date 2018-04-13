using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShadowJimmy : MonoBehaviour {
    public enum Mode {Run, Hide};

    NavMeshAgent nav;
    float fov = 120f;
    Vector3 lastSeen;

    public Transform target;
    public Rigidbody rb;
    float speed = 9f;

    public Mode mode;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Destination").transform;
        nav = GetComponent<NavMeshAgent>();
        SetMode(Mode.Hide);
        StartCoroutine(SJBehaviour());
    }

    // Update is called once per frame
    void Update () {

        switch (mode)
        {
            case Mode.Run:
                break;
            case Mode.Hide:
                nav.SetDestination(target.position);
                break;
        }

    }

    IEnumerator SJBehaviour()
    {
        while(true)
        {
            if(mode == Mode.Run)
            {
                Vector3 destination = transform.position + new Vector3(Random.Range(-10, 10), 0f, Random.Range(-10, 10));
                nav.SetDestination(destination);
            }
            yield return new WaitForSeconds(Random.Range(3,7));
        } 
    }
    void SetMode(Mode m)
    {
        mode = m;
        switch (mode)
        {
            case Mode.Run:
                break;
        }
    }
}


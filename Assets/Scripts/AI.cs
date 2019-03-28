using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AI : MonoBehaviour
{
    NavMeshAgent _agent;
    Vector3 _target;
    GameObject _player;


    public float fieldOfViewDegrees;
    public float visibilityDistance;


    public Vector3 test;
    enum AiState
    {
        Walking,
        Running,
        Searching //for enemy to shoote
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer())
        {
            Debug.Log("I can see the player");
        }
        DrawDebugFOV();
    }

    bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 rayDirection = _player.transform.position - transform.position;


        if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewDegrees * 0.5f)
        {
            // Detect if player is within the field of view
            if (Physics.Raycast(transform.position, rayDirection, out hit, visibilityDistance))
            {
                return true;
            }
        }
        return false;
    }

    //this is WIP
    void DrawDebugFOV()
    {
        Vector3 rayDirection = _player.transform.position - transform.position;

        Debug.DrawLine(transform.position,rayDirection - test);
        
    }

}

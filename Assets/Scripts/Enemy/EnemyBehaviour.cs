using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //needed to word with the AI components

public class EnemyBehaviour : MonoBehaviour
{
    NavMeshAgent nav; //NavMeshAgent (nav) on enemy

    public Transform target;
    
    

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.destination = target.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //switch
        }
    }
}

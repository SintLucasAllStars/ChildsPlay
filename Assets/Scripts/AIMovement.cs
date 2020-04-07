using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    //navmeshagent
    NavMeshAgent nav;

    //player object
    public GameObject player;

    //enemy prefab (homeowner
    public GameObject angryHomeOwner;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        nav.destination = player.transform.position;
    }
}
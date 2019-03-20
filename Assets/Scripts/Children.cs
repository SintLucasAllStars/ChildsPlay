using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Children : MonoBehaviour
{
    public NavMeshAgent agent;
    public float distance;
    private float posX;
    private float posZ;
    Vector3 Rlocation;
    GameObject Player;
    Vector3 posPlayer;
    Vector3 newLocation;


        void Start()
    {
        //palt de navmeshagent voor movement en maakt een nieuwe random locatie aan om naar toe tegaan
        agent = gameObject.GetComponent<NavMeshAgent>();
        Rlocation = new Vector3(posX, -3.87f, posZ);
        //vind de player 
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        //pakt de player transform
        posPlayer = Player.GetComponent<Transform>().position;
        //loopt naar de nieuwe positie
        agent.SetDestination(Rlocation);
        //berekent hoe ver de player van he kind vandaan is 
        distance = Vector3.Distance(transform.position, posPlayer);
        if(distance < 3)
        {
            ChangePosCoords();
            Rlocation = new Vector3(posX, -3.87f, posZ);
            Debug.Log("WEEWOO");
            return;

        }



        //debug .log if Random pos
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            ChangePosCoords();
            Rlocation = new Vector3(posX, -3.87f, posZ);
        }
    }
    // nieuwe positie aanmaken
    void ChangePosCoords()
    {
        posX = Random.Range(-4, 4);
        posZ = Random.Range(-4, 4);
    }
}

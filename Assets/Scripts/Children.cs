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
    bool changeLocation;
    Vector3 Rlocation;
    GameObject Player;
    Vector3 posPlayer;
    Vector3 newLocation;
   




    public float multiplyBy;


        void Start()
    {
        changeLocation = false;
        multiplyBy = 0.2f;
      
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
      //  agent.SetDestination(Rlocation);
        //berekent hoe ver de player van he kind vandaan is 
        distance = Vector3.Distance(transform.position, posPlayer);
        if(distance < 5)
        {
                      
            move();
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(transform.position, forward, Color.green);
            agent.Stop();
            changeLocation = true;
            if (changeLocation)
            {
                posX = Random.Range(-4, 4);
                posZ = Random.Range(-4, 4);
                Rlocation = new Vector3(posX, -3.87f, posZ);
                changeLocation = false;
            }



        }
        else
        {
     
            agent.SetDestination(Rlocation);
            agent.Resume();

        }
       


        //debug .log if Random pos
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
           
            Rlocation = new Vector3(posX, -3.87f, posZ);
        }
    }
    // nieuwe positie aanmaken
    void move()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - posPlayer).normalized;
        transform.Translate(0, 0, 0.02f);
       // Vector3 RunTo = transform.position + transform.forward * multiplyBy;
    }
   
    
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Children : MonoBehaviour
{
    public NavMeshAgent agent;
    public float distance;
    public float posX;
    public float posZ;
    bool changeLocation;
    Vector3 Rlocation;
    GameObject Player;
    Vector3 posPlayer;
    Vector3 newLocation;
    bool m_allowedMove = true;

    private GameObject Manager;
    public float m_speed = 0.02f;






    public float multiplyBy;


        void Start()
    {
        changeLocation = false;
        multiplyBy = 0.2f;
      
        //palt de navmeshagent voor movement en maakt een nieuwe random locatie aan om naar toe tegaan
        agent = gameObject.GetComponent<NavMeshAgent>();
        Rlocation = new Vector3(Random.Range(-6, 6), -3.87f, Random.Range(-6, 6));
        //vind de player 
        Player = GameObject.FindGameObjectWithTag("Player");

        Manager = GameObject.FindGameObjectWithTag("K_m");

        StartCoroutine("ChangeCoords");
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
        if(distance < 5 && m_allowedMove)
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
       


    }
    // nieuwe positie aanmaken
    void move()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - posPlayer).normalized;
        transform.Translate(0, 0, m_speed);
       // Vector3 RunTo = transform.position + transform.forward * multiplyBy;
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
        
            
            //gameObject.GetComponent<Renderer>().material.color = Color.black;
            m_allowedMove = false;
            agent.Stop();
            gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
             Manager.GetComponent<ChildManager>().IncreaseSpeed();
            //send msg to GameManager
        }
    }
    IEnumerator ChangeCoords()
    {
       
        posX = Random.Range(-4, 4);
        posZ = Random.Range(-4, 4);
        Rlocation = new Vector3(posX, -3.87f, posZ);
        agent.SetDestination(Rlocation);
        yield return new WaitForSeconds(9);
        
    }
  



}

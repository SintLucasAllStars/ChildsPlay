using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Children : MonoBehaviour
{
    public NavMeshAgent agent;
    private float distance;
    private float posX;
    private float posZ;
    Vector3 Rlocation;
    GameObject Player;


        void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        Rlocation = new Vector3(posX, -3.87f, posZ);
        Player = find
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(Rlocation);
      

        if ())
        {
            ChangePosCoords();
            Rlocation = new Vector3(posX, -3.87f, posZ);
        }
    }
    void ChangePosCoords()
    {
        posX = Random.Range(-4, 4);
        posZ = Random.Range(-4, 4);
    }
}

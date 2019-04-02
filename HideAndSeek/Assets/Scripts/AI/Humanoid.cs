using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Humanoid : MonoBehaviour
{
   
    NavMeshAgent agent;
    public float speed;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
       
       
    }

   
    protected void MoveTo(Vector3 Pos)
    {
        agent.destination = Pos;
    }


}

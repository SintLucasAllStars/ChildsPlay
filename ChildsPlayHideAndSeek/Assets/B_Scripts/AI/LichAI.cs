using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LichAI : MonoBehaviour
{

    public NavMeshAgent agent;

    private GameObject player;

    private Vector3 randomDir;
    private Vector3 finalPos;

    public GameObject fireBall;
    public GameObject ballSpawner;

    private float walkRadius = 20.0f;

    private bool canAttack;

    private void Awake()
    {
        finalPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Slime");
        canAttack = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Attack();
    }

    private void Movement()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    randomDir = Random.insideUnitSphere * walkRadius;
                    randomDir += transform.position;
                    NavMeshHit hit;
                    NavMesh.SamplePosition(randomDir, out hit, walkRadius, 1);
                    finalPos = hit.position;
                }
            }
        }

        agent.SetDestination(finalPos);

        transform.LookAt(player.transform.transform);
    }

    private void Attack()
    {
        if (canAttack == true)
        {
            Debug.Log("shoot");
            Instantiate(fireBall, ballSpawner.transform.position, ballSpawner.transform.rotation);
            canAttack = false;
            StartCoroutine("AttackCooldown");
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(3.0f);
        canAttack = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public enum States
    {
        Patrol, Chase, Rage
    }

    public Transform target;
    NavMeshAgent nvmesh;
    public float fov;
    public float range;
    public float startCountdown = 3;
    public float rageCountdown = 10;
    public States currentState = States.Patrol;
    public bool gameStarted = false;

    void Start()
    {
        nvmesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startCountdown > 0)
        {
            startCountdown -= Time.deltaTime;
        }
        else
        {
            gameStarted = true;
        }

        if (gameStarted)
        {
            if (rageCountdown < 0)
            {
                currentState = States.Rage;
            }

            if (SeesTarget() && currentState != States.Rage)
            {
                currentState = States.Chase;
            }
            else if (currentState != States.Rage)
            {
                currentState = States.Patrol;
            }

            switch (currentState)
            {
                case States.Patrol:
                    range = 10;
                    fov = 150;
                    nvmesh.speed = 3.5f;
                    rageCountdown -= Time.deltaTime;

                    if (Time.frameCount % 300 == 0)
                    {
                        Vector3 newDest = transform.position + new Vector3(Random.Range(-10f, 10f), transform.position.y, Random.Range(-10f, 10f)) * 4;
                        nvmesh.destination = newDest;
                    }
                    break;

                case States.Chase:
                    range = 10;
                    fov = 150;
                    nvmesh.speed = 4f;
                    nvmesh.destination = target.position;
                    rageCountdown = 10;
                    break;

                case States.Rage:
                    range = 100;
                    fov = 360;
                    nvmesh.speed = 6f;
                    rageCountdown -= Time.deltaTime;
                    if (rageCountdown < -10)
                    {
                        currentState = States.Chase;
                    }

                    nvmesh.destination = target.position;
                    break;

                default:
                    break;

            }
        }
    }

    public bool SeesTarget()
        {
            Vector3 p = target.position - transform.position;
            float angle = Vector3.Angle(p, transform.forward);
            if (angle <= fov / 2.0f)
            {
                RaycastHit hit;

                if (Physics.Raycast(origin: transform.position, direction: p, out hit, range))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
}

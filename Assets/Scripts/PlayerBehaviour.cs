using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject soundPing;

    private Camera cam;

    private NavMeshAgent agent;

    private RaycastHit hit;

    private bool crounching;
    GameObject sound = null;


    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                sound = Instantiate(soundPing, hit.point, Quaternion.identity);
                Destroy(sound, 0.1f);
            }
        }

        #region
        //crouching
        #region crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (crounching)
            {
                crounching = false;

                transform.position = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z);
                transform.localScale = new Vector3(1, 1, 1);

                agent.speed *= 2;
                agent.acceleration *= 2;
                agent.angularSpeed *= 2;
            }
            else
            {
                crounching = true;

                transform.localScale = new Vector3(1, 0.5f, 1);
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);

                agent.speed /= 2;
                agent.acceleration /= 2;
                agent.angularSpeed /= 2;
            }
        }
        #endregion
        #region sprint
        if (!crounching)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                agent.speed *= 2;
                agent.acceleration *= 2;
                agent.angularSpeed *= 2;
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                if (sound == null)
                {
                    sound = Instantiate(soundPing, transform.position, Quaternion.identity);
                    Destroy(sound, 0.1f);
                }
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                agent.speed /= 2;
                agent.acceleration /= 2;
                agent.angularSpeed /= 2;
            }
        }
        #endregion
        #endregion
    }
}
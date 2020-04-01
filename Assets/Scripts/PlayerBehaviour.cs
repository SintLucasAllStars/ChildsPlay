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
               GameObject sound = Instantiate(soundPing, hit.point, Quaternion.identity);
                Destroy(sound, 0.1f);
            }
        }
    }
}
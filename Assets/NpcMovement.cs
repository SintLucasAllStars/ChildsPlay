using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;

public class NpcMovement : MonoBehaviour
{
    private NavMeshAgent nav;
    public Transform objTransform;
    //public GameObject Player;
    float range = 10;
    float fov = 150;
    public Transform firePoint;
    public States currentState = States.Patrol;

    public ThirdPersonCharacter character;

    public Transform cameraTransform;
    public GameObject alertImage;

    public enum States
    {
        Patrol,
        Chase
    }
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        //Player = GameObject.Find("Player");
        nav.updateRotation = false;
        alertImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.LookAt(cameraTransform);
        alertImage.transform.LookAt(cameraTransform);

        if (SeesTarget())
        {
            currentState = States.Chase;
        }
        else
        {
            currentState = States.Patrol;
        }

        switch (currentState)
        {
            case States.Patrol:
                if (Time.frameCount % 60 == 0)
                {
                    alertImage.SetActive(false);
                    Vector3 newDest = transform.position + new Vector3(Random.Range(-15f, 15f), transform.position.y, z: Random.Range(-15f, 15f) * 6f);
                    GetComponent<NavMeshAgent>().speed = 0.4f;
                    nav.destination = newDest;
                }
                break;
            case States.Chase:
                alertImage.SetActive(true);
                nav.destination = objTransform.position;
                GetComponent<NavMeshAgent>().speed = 0.8f;
                break;

        }
        //objTransform = Player.gameObject.GetComponent<Transform>();
        //nav.destination = objTransform.position;
        //nav.isStopped = nav.remainingDistance > range;
        Debug.DrawRay(firePoint.position, firePoint.forward * range);

        if(nav.remainingDistance > nav.stoppingDistance)
        {
            character.Move(nav.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
    }
    public bool SeesTarget()
    {
        Vector3 p = objTransform.position - transform.position;
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

using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections;

public class NpcMovement : MonoBehaviour
{
    private NavMeshAgent nav;
    public Transform objTransform;
    //public GameObject Player;
    public float range = 10;
    float fov = 150;
    public Transform firePoint;
    public States currentState = States.Patrol;
    public bool seesNpc = false;
    private bool npcInRange = false;

    public ThirdPersonCharacter character;

    public Transform cameraTransform;
    public GameObject alertImage;
    private GameObject obj;

    public Material blue;
    public Material red;
    public Material black;
    public Material green;

    private new SkinnedMeshRenderer renderer;

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

        renderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();//gives every npc a random color
        var randomColor = Random.Range(0, 5);
        switch (randomColor)
        {
            case 1:
                renderer.material = red;
                break;
            case 2:
                renderer.material = blue;
                break;
            case 3:
                renderer.material = black;
                break;
            case 4:
                renderer.material = green;
                break;
        }

        objTransform = GameObject.FindWithTag("Player").transform;
        cameraTransform = GameObject.FindWithTag("MainCamera").transform;
        nav.updateRotation = false;
        alertImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (seesNpc == true)//other npc is in range
        {
            alertImage.SetActive(true);//puts a black triangle on top of the npc's head
            nav.destination = objTransform.position;//makes the npc goes after the player
            GetComponent<NavMeshAgent>().speed = 0.8f;//changes the movement speed of the npc
        }

        alertImage.transform.LookAt(cameraTransform);

        if (SeesTarget())//state machine
        {
            currentState = States.Chase;

        }
        else
        {
            currentState = States.Patrol;
            GetComponent<SphereCollider>().enabled = false;
        }

        switch (currentState)
        {
            case States.Patrol:
                if (Time.frameCount % 60 == 0)//makes the npc walk random
                {
                    alertImage.SetActive(false);
                    Vector3 newDest = transform.position + new Vector3(Random.Range(-15f, 15f), transform.position.y, z: Random.Range(-15f, 15f) * 6f);
                    GetComponent<NavMeshAgent>().speed = 0.4f;
                    nav.destination = newDest;
                }
                break;
            case States.Chase://the chase state of the npc
                alertImage.SetActive(true);
                nav.destination = objTransform.position;
                GetComponent<NavMeshAgent>().speed = 0.8f;
                break;

        }
        Debug.DrawRay(firePoint.position, firePoint.forward * range);//draws visual indication of field of view

        if (nav.remainingDistance > nav.stoppingDistance)
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
                    GetComponent<SphereCollider>().enabled = true;
                    if (npcInRange == true)
                    {
                        NpcManager.Instance.AlertMessage();//sent out a call request towards other npc's to set new follow target
                    }
                    return true;
                }

            }
        }
        return false;
    }

    public void TakeStunEffect()
    {
        //the enemy can't move for 6 seconds
        this.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        StartCoroutine("Timer");
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(6);
        this.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }

    public void StartChase()
    {
        seesNpc = true;//all npc's go into chase mode
        print($"change state to chase: {this.gameObject}");
    }
    public void StopChase()//stops chase mode after timer
    {
        seesNpc = false;
    }
    public void OnTriggerEnter(Collider other)//checks if other npc is in range. if other npc in range and sees player. 
    {
        if (other.CompareTag("Enemy"))
        {
            npcInRange = true;

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            npcInRange = false;
        }
    }
}

using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

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

        renderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
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
            //sent out a call request towards other npc's to set new follow target
            //NpcManager.Instance.AlertMessage();
            alertImage.SetActive(true);
            nav.destination = objTransform.position;
            GetComponent<NavMeshAgent>().speed = 0.8f;
            //refrence naar andere npc krijgen,verander target
            //seesNpc = false;
        }

        alertImage.transform.LookAt(cameraTransform);

        if (SeesTarget())
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
                    GetComponent<SphereCollider>().enabled = true;
                    if (npcInRange == true)
                    {
                        NpcManager.Instance.AlertMessage();
                    }
                    return true;
                }

            }
        }
        return false;
    }

    public void StartChase()
    {
        //de state veranderd. code moet wel naar update
        seesNpc = true;//iedere NPC volgt player
        print($"change state to chase: {this.gameObject}");
    }
    public void StopChase()
    {
        seesNpc = false;
    }
    public void OnTriggerEnter(Collider other)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature_Maneger : MonoBehaviour
{

    //objects needed acces
    private TerrainGenerator terrainGenerator;
    private World_Maneger world_Maneger;

    #region OOP part
    private AI_Class aI_Class;
    public AI_Class.Type TypeKid;
    public static int AmountOfChaser;
    private float stamina, baseStamina; // this works but i dont know how and why
    [SerializeField] private float speed;
    private float fov;
    #endregion

    #region AI
    private enum State { Chase, Scarecrow, running, walking, hiding }
    [SerializeField] private State myState;
    private NavMeshAgent agent;
    private int width;
    public GameObject[] hidingplaces;
    private Vector3 target;
    private Vector3 targetTagger;
    #endregion

    #region cozmetic stuff
    //i really dont know how to spell that word
    private Light mylight;
    //the colors will display the state of the player as a visual repensentation
    [SerializeField] [Tooltip("0 = Hidden  1 = running  2 = Scarecrow")] private Color[] lightColors = new Color[3];
    #endregion

    // Use this for initialization
    void Start()
    {
        //setup oop
        aI_Class = new AI_Class(1);

        //setup Navmesh
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;

        //objects needed acces
        terrainGenerator = GameObject.Find("World").GetComponent<TerrainGenerator>();
        world_Maneger = GameObject.Find("World").GetComponent<World_Maneger>();
        mylight = GetComponent<Light>();

        //setting variable
        myState = State.running;
        width = terrainGenerator.width;
        hidingplaces = new GameObject[world_Maneger.AmountOfObstacles];

        //running initial functions
        NewPosition();
        GetOOp();

        // checking if there are other chasers

    }

    // Update is called once per frame
    void Update()
    {
        Stamina();
    }

    #region General functions
    //takes of stamina and stamina regen.
    void Stamina()
    {
        if (stamina <= 0 && myState == State.running)
        {
            myState = State.walking;
            agent.speed = speed / 2;
        }

        if (myState == State.running)
        {
            stamina -= Time.deltaTime / 3.5f;

        }

        if (myState == State.walking)
        {
            stamina += Time.deltaTime * 1.5f;
            if (stamina >= baseStamina)
            {
                myState = State.running;
                agent.speed = speed;
            }
        }
    }

    //is used for any navmesh agent that needs a new position inside of the world
    void NewPosition()
    {
        if (agent.isOnNavMesh)
        {
            if (agent.remainingDistance <= 0)
            {
                float x = Random.Range(0, width);
                float z = Random.Range(0, width);
                float y = terrainGenerator.ReturnHeight(x, z);
                target = new Vector3(x, y, z);
                agent.SetDestination(target);
            }
        }
    }
    #endregion

    #region ChaserSpecific
    void ChasePlayer()
    {
        if (myState == State.Chase)
        {
            agent.SetDestination(targetTagger);
            Debug.Log(targetTagger);
        }
    }

    void HidersFinding()
    {
        RaycastHit hit;
        Vector3 direction = target - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Tagger"))
            {
                float angleT = Vector3.Angle(transform.forward, direction);
                if (fov < angleT / 2)
                {
                    myState = State.Chase;
                    targetTagger = hit.transform.position;
                }
            }
        }
    }
    #endregion

    //gets all the oop vars for this particeuler creature
    void GetOOp()
    {
        TypeKid = aI_Class.TypeKid;
        if (TypeKid == AI_Class.Type.Chaser)
        {
            if (AmountOfChaser > 1)
            {
                TypeKid = AI_Class.Type.FastKid;
            }
            else
            {
                AmountOfChaser++;
            }
        }

        stamina = aI_Class.stamina;
        baseStamina = aI_Class.stamina;
        speed = aI_Class.speed;
        agent.speed = speed;
        fov = aI_Class.fov;
    }
}

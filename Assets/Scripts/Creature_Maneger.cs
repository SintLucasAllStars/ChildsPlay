using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature_Maneger : MonoBehaviour
{

    //objects needed acces
    private TerrainGenerator terrainGenerator;

    #region OOP part
    private AI_Class aI_Class;
    public AI_Class.Type TypeKid;
    [SerializeField] public static int Chasers;
    public bool isChaser;
    private float stamina;
    private float baseStamina;
    private float speed;
    private float reactionSpeed;
    private float fov;
    #endregion

    #region AI
    public enum State { Chase, Scarecrow, running, walking, hiding }
    public State myState;
    private NavMeshAgent agent;
    private Transform[] hidingplaces;
    public Vector3 target;
    #endregion

    #region cozmetic stuff
    //i really dont know how to spell that word
    private Light mylight;
    #endregion

    // Use this for initialization
    void Start()
    {
        aI_Class = new AI_Class(1);
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        terrainGenerator = GameObject.Find("World").GetComponent<TerrainGenerator>();
        myState = State.running;

        TargetUpdate();
        GetOOp();
    }

    // Update is called once per frame
    void Update()
    {
        TargetUpdate(); // this should be removed before the final release 
        Stamina();
    }

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
                }
            }
        }
    }

    void TargetUpdate()
    {
        if (agent.isOnNavMesh)
        {
            if (agent.remainingDistance <= 0)
            {
                float x = Random.Range(0, 250);
                float z = Random.Range(0, 250);
                float y = terrainGenerator.ReturnHeight(x, z);
                target = new Vector3(x, y, z);
                agent.SetDestination(target);
            }
        }
    }

    void GetOOp()
    {
        TypeKid = aI_Class.TypeKid;
        isChaser = aI_Class.isChaser;
        stamina = aI_Class.stamina;
        baseStamina = aI_Class.stamina;
        speed = aI_Class.speed;
        agent.speed = speed;
        reactionSpeed = aI_Class.reactionSpeed;
        fov = aI_Class.fov;
    }
}

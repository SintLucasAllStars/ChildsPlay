using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]

    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             
        public ThirdPersonCharacter character { get; private set; } 

        GameObject targetobject;

        Transform[] hidingspots;
        int index;

        public LayerMask targetmask;

        public GameObject placeholderdestprefab;

        public int prevtarget;

        float timeprevious;

        public Vector3 lastknownplayerpos;

        public bool playercoll = false;


        public GameObject seeker1;
        public Transform seekerr;


        GameObject[] hidespots;


        float curtime;
        float curtime1;

        [SerializeField] private float m_RunSpeed;

   
        Vector3 oldspot;


        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = false;
            agent.updatePosition = true;

            hidespots = GameObject.FindGameObjectsWithTag("hidespot");

            agent.SetDestination(getrandomtarget());

            curtime = Time.time;
            curtime1 = Time.time;

            InvokeRepeating("OnTriggerEnter", 1.0f, 2.0f);
            InvokeRepeating("HitByRay", 1.0f, 2.0f);

        }


        private void Update()
        {

            if (Mathf.Abs(agent.remainingDistance - agent.stoppingDistance) < 1 && (Time.time - timeprevious > 1) && playercoll)
            {
                timeprevious = Time.time;
                agent.SetDestination(getClosestDest());
                playercoll = false;

            }
            else // als t minder dan 1 seconde duurt
            {
                character.Move(agent.desiredVelocity, false, false);
            }

        }


        private Vector3 getClosestDest()
        {

            int randompos = UnityEngine.Random.Range(1, 10);
            float step = -1 * Time.deltaTime;
            
           // Vector3 possie = new Vector3(seeker1.transform.position.x + -seekerr.transform.position.x, seeker1.transform.position.y, seeker1.transform.position.z + -seekerr.transform.position.z);

            GameObject[] closehidingspots = GameObject.FindGameObjectsWithTag("hidespot");
            Vector3 curclosesthidingspot = transform.position;
 
            float disttoclosesthiddingspot = 999999;

            foreach (GameObject closehidingspot in closehidingspots)
            {

                float disttohidingspot = Vector3.Distance(closehidingspot.transform.position, transform.position);

                if (disttohidingspot < disttoclosesthiddingspot && Vector3.Distance(transform.position, closehidingspot.transform.position) > 7 )
                {
                    oldspot = transform.position;
                    curclosesthidingspot = closehidingspot.transform.position;
                    disttoclosesthiddingspot = disttohidingspot;
                }
            }
            playercoll = false;
            return curclosesthidingspot;
        }


        private Vector3 getrandomtarget()
        {

            int targetpos = UnityEngine.Random.Range(0, hidespots.Length);

            while (prevtarget == targetpos)
            {
                hidespots = GameObject.FindGameObjectsWithTag("hidespot");
                targetpos = UnityEngine.Random.Range(0, hidespots.Length);
            }
            prevtarget = targetpos;
            GameObject targetobject = hidespots[targetpos];

            Vector3 ObjPos = targetobject.transform.position;
            Vector3 SeekPos = seeker1.transform.position;

            Vector3 DesVect = SeekPos - ObjPos;

            Vector3 DesPos = ObjPos + (DesVect.normalized * -1);

            return DesPos;
        }

        //private Vector3 goOtherSide()
        //{


        //    GameObject targetobject = 

        //    Vector3 ObjPos = targetobject.transform.position;
        //    Vector3 SeekPos = seeker1.transform.position;

        //    Vector3 DesVect = SeekPos - ObjPos;

        //    Vector3 DesPos = ObjPos + (DesVect.normalized * -1);
        //}

        private void OnTriggerEnter(Collider other)
        {


            // Debug.Log("out");

            if (other.tag == "Player")
            {
                agent.SetDestination(getrandomtarget());
                //geraakt();
                playercoll = true;
            }
        }


        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player")
            {
                //geraakt();
                agent.SetDestination(getrandomtarget());
                playercoll = true;

            }
        }


        void geraakt()
        {
            Debug.Log("Go somewhere else");
            agent.SetDestination(getClosestDest());
        }


        void HitByRay()
        {
                Debug.Log("I was hit by a Ray");
            // agent.SetDestination(getClosestDest());
            //  agent.SetDestination(getClosestDest());  
            // agent.SetDestination(getrandomtarget());
            //geraakt();
           //maak ray kleiner en add dat die persoon af is.
        }
    }



}
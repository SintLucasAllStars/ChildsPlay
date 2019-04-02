using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]

    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling

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
          //  InvokeRepeating("OnTriggerStay", 1.0f, 3.0f);
            
            //  getClosestDest();

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
            //agent.SetDestination(seekerr.transform.position);
            // Vector3 possie = Vector3.MoveTowards(transform.position, seekerr.position, step);
            Vector3 possie = new Vector3( seeker1.transform.position.x + -seekerr.transform.position.x ,seeker1.transform.position.y, seeker1.transform.position.z + -seekerr.transform.position.z);


            GameObject[] closehidingspots = GameObject.FindGameObjectsWithTag("hidespot");
            Vector3 curclosesthidingspot = transform.position;
            float disttoclosesthiddingspot = 999999;

           foreach (GameObject closehidingspot in closehidingspots)
            {

                float disttohidingspot = Vector3.Distance(closehidingspot.transform.position,transform.position);

                if (disttohidingspot < disttoclosesthiddingspot && Vector3.Distance(transform.position,closehidingspot.transform.position) > 7 )
                {
                    curclosesthidingspot = closehidingspot.transform.position;
                    disttoclosesthiddingspot = disttohidingspot;
                }



            }


            //Debug.Log("ga weg");



            //agent.SetDestination(seekerr.transform.position);
            // Vector3 possie = Vector3.MoveTowards(transform.position, seekerr.position, step);
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

            Vector3 DesPos = ObjPos + (DesVect.normalized * -1); //define "radius" later - now 4
            //targetobject.transform.position = DesPos.normalized * 4; //define "radius" later - now 4
           // Debug.Log(DesPos);

            return DesPos; 

        }


        private void OnTriggerEnter(Collider other)
        {


           // Debug.Log("out");

                if (other.tag == "Player")
                {
                    agent.SetDestination(getClosestDest());
                    playercoll = true;
                   // Debug.Log("On trigger enter");
                   

                }


        }

        //private void OnTriggerStay(Collider other)
        //{

        //    if (other.tag == "Player")
        //    {
        //        agent.SetDestination(getClosestDest());
        //        playercoll = true;

        //    }


        //}

        private void OnTriggerExit(Collider other)
        {

            //

        }

        void HitByRay()
        {

           if ((curtime - Time.time) > 1)
            {

                Debug.Log("I was hit by a Ray");
                // agent.SetDestination(getClosestDest());
                agent.SetDestination(getClosestDest());

                curtime = Time.time;
            }

        }
    }



}

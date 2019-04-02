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








        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = false;
            agent.updatePosition = true;

            agent.SetDestination(getrandomtarget()); // zeg tegen agent --> ga naar vector 3 getrandomtarget boi


        }


        private void Update()
        {


            if (Mathf.Abs(agent.remainingDistance - agent.stoppingDistance) < 1 && (Time.time - timeprevious > 1) && playercoll) // stopping distance = afwijkende aantal wat de speler van de orginele target af kan zitten
            {
                timeprevious = Time.time;
                agent.SetDestination(getrandomtarget());
                playercoll = false;

            }
            else // als t minder dan 1 seconde duurt
            {
                character.Move(agent.desiredVelocity, false, false);

            }


        }



        private Vector3 getrandomtarget()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("hidespot"); //array met alle hidingspots --> hidespot tag
            int targetpos = UnityEngine.Random.Range(0, objs.Length); // random int van 0 tot de lengte array

            if (prevtarget == targetpos) // als vorige positie tzelfde is als gekozen positie, kies een nieuwe
            {
                objs = GameObject.FindGameObjectsWithTag("hidespot");
                targetpos = UnityEngine.Random.Range(0, objs.Length);
            }

            prevtarget = targetpos; // sla if eerste run over --> targetpos gaat in prevtarget, je slaat op welke je hebt gehad

            GameObject targetobject = objs[targetpos]; // je pakt de randomspot in de objs array en stopt die in targetobject
            return targetobject.transform.position; // return de positie van targetobject

        }
        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Player")
            {

                playercoll = true;

                Debug.Log("collie");
            }

        }

        private void OnTriggerExit(Collider other)
        {

            //

        }


    }



}


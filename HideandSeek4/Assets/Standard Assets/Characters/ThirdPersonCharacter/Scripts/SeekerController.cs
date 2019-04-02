using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class SeekerController : MonoBehaviour
    {
		
		
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
		public Transform target; // target to aim for
		
		public GameObject[] hiders;
		public GameObject[] hidingPlaces;
		
		bool isVisible;
		bool investigating;


		public float range;
		public float fov;

		Vector3 investigatePoint;
		Vector3 lastKnowPositionVec;
		
		public enum Mode
		{
			Seeking,   //Seeking for hiders. Moving between hiding places. 
			Chasing,  //Chasing current hider in sight. 
			InvestigatingLKP, //LKP = LastKnownPosition. Moving towards LKP. 
			InvestigatingAroundLKP, //Investigating in a circle around LKP 
		}

		public Mode seekingMode;

		private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
			hiders = GameObject.FindGameObjectsWithTag("Hider");
			hidingPlaces = GameObject.FindGameObjectsWithTag("Terrain");
			
	        agent.updateRotation = false;
	        agent.updatePosition = true;
			isVisible = false;
			
			setMode(Mode.Seeking);
			
							
        }

        private void Update()
        {
			Move();

			for (int i = 0; i < hiders.Length; i++)
			{
				

				if (TargetVisible(hiders[i]))
				{
					setMode(Mode.Chasing);
					isVisible = true;
					SetTarget(hiders[i].transform);
					break;
				}
				else
				{
					isVisible = false;
				}
					
				
			}
			
			

			switch (seekingMode)
			{
				case Mode.Seeking:
					if(agent.remainingDistance < 1)
					{
						Vector3 hidingSpot = RandomHidingSpot();
						agent.SetDestination(hidingSpot + new Vector3(UnityEngine.Random.Range(-2f, 2f), 0f, UnityEngine.Random.Range(-2f, 2f)));
					}
					break;
				case Mode.Chasing:
					if(isVisible)
					{
						
						agent.SetDestination(target.transform.position);
						agent.speed = 1f;
						lastKnowPositionVec = target.position;
					}
					else
					{
						
						setMode(Mode.InvestigatingLKP);
						agent.speed = 0.75f;
					}
					break;
				case Mode.InvestigatingLKP:
					SetTarget(null);
					//InvestigatingLKP // Store position of target at the moment when the target is no longer visible.     
					agent.SetDestination(lastKnowPositionVec);
					if(Vector3.Distance(transform.position, lastKnowPositionVec) < 1f)
					{
						setMode(Mode.InvestigatingAroundLKP);
					}
					break;
				case Mode.InvestigatingAroundLKP:
					//Investigating around the last known position. 

					break;
			}

		}

		private void Move()
		{
			if (agent.remainingDistance > agent.stoppingDistance)
				character.Move(agent.desiredVelocity, false, false);
			else
				character.Move(Vector3.zero, false, false);
		}		
						
		void setMode(Mode m)
		{
			switch (m)
			{
				case Mode.Seeking:
					Vector3 hidingSpot = ClosestHidingSpot(transform.position);
					agent.SetDestination(hidingSpot + new Vector3(UnityEngine.Random.Range(-2f, 2f), 0f, UnityEngine.Random.Range(-2f, 2f)));
					break;
				case Mode.Chasing:
					break;
				case Mode.InvestigatingLKP:
					agent.SetDestination(lastKnowPositionVec);
					break;
				case Mode.InvestigatingAroundLKP:
					//After couple of seconds return to seeking.
					setMode(Mode.Seeking);
					break; 
			}
			seekingMode = m;
		}
				
		Vector3 ClosestHidingSpot(Vector3 pos)
		{
			float shortestDistance = 1000;
			Vector3 closest = new Vector3();

			for (int i = 0; i < hidingPlaces.Length; i++)
			{
				float dist = Vector3.Distance(pos, hidingPlaces[i].transform.position);
				if(dist < shortestDistance)
				{
					shortestDistance = dist;
					closest = hidingPlaces[i].transform.position;
				}
			}
			return closest;
		}

		Vector3 RandomHidingSpot()
		{
			return hidingPlaces[UnityEngine.Random.Range(0, hidingPlaces.Length)].transform.position;
		}

		Vector3 LastKnowPosition()
		{
			
			if (!isVisible)
			{
				target.position = lastKnowPositionVec;
				
			}

			return lastKnowPositionVec;
		}
			   		
		bool TargetVisible(GameObject hider)
		{
			Vector3 v = hider.transform.position - transform.position;
			Ray ray = new Ray(transform.position, v);
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(ray, out hit, range))
			{
				if (hit.collider.gameObject == hider)
				{
					
					float angle = Vector3.Angle(transform.forward, v);
								
					if(angle < fov / 2)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
		
		public void SetTarget(Transform target)
        {
			this.target = target;
        }

		
    }
}

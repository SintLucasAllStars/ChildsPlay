using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIBehaviour : MonoBehaviour {
	public enum BodyType {Fat, Normal, Aggressive};
	public BodyType bodyType;

	public NavMeshAgent agent;
	public CentralIntelligence centralIntelligence;

	public float normalSpeed;
	public Vector3 size;
}
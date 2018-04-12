using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : AIBehaviour {
	public enum GuardMode {Normal, Search, Chase};
	public GuardMode guardMode;

	public CentralIntelligence centralIntelligence;

	public float searchSpeed;
	public float chaseSpeed;


	void Start () {
		switch (bodyType) {
		case BodyType.Fat:
			normalSpeed = 0.40f;
			searchSpeed = 0.40f;
			chaseSpeed = 0.70f;
			transform.localScale = new Vector3 (2.5f, 1.8f, 2.5f);
			break;
		case BodyType.Normal:
			normalSpeed = 0.50f;
			searchSpeed = 0.80f;
			chaseSpeed = 1;
			transform.localScale = new Vector3 (1.8f, 1.8f, 1.8f);
			break;
		case BodyType.Aggressive:
			normalSpeed = 0.60f;
			searchSpeed = 1;
			chaseSpeed = 1;
			transform.localScale = new Vector3 (1.8f, 2f, 1.8f);
			break;
		}
	}

	void Update () {
		
	}

	public void SetMode () {
		
	}
}

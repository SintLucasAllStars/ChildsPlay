using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour {

	public GameObject transf1;
	public GameObject transf2;

	public Vector3 dir;

	void Start(){

		dir = transf2.transform.position - transf1.transform.position;

	}

}
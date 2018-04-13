using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour {

	PlayerBehaviour player;

	void Awake(){
		player = transform.parent.GetComponent<PlayerBehaviour>();
	}

	void OnTriggerStay(Collider c){
		if (c.gameObject.isStatic)
			player.isGrounded = true;
	}

	void OnTriggerExit(Collider c){
		if (c.gameObject.isStatic)
			player.isGrounded = false;
	}
}

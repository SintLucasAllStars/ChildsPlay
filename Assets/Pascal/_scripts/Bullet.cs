using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	float downwardsGravity;
	float gravityReducer;
	public AudioClip impact;
	// Use this for initialization
	void Start () {
		downwardsGravity = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		downwardsGravity = Mathf.Pow (downwardsGravity, downwardsGravity);
		transform.position = transform.position + Vector3.forward;
}

	void OnCollisionEnter(Collision coll){
		AudioSource.PlayClipAtPoint(impact,transform.position);
		Destroy (this.gameObject);
	}
}
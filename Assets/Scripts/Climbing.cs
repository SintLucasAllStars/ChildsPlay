using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour {
    public bool canClimb;
    Rigidbody rb;
    public Animator anim;
    public Camera regularCam;
    public Camera parcourCam;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
        if (canClimb && Input.GetKeyDown(KeyCode.Space))
        {
            regularCam.depth = 0;
            parcourCam.depth = 1;
            rb.isKinematic = true;
            anim.SetTrigger("Climbup");
            StartCoroutine(afterClimb());
        }
	}
    IEnumerator afterClimb(){
        yield return new WaitForSeconds(1);
        regularCam.depth = 1;
        parcourCam.depth = 0;
        rb.isKinematic = false;
        transform.position = parcourCam.transform.position;
    }
    void OnTriggerEnter (Collider other){
        canClimb = true;
    }
    void OnTriggerExit (Collider other){
        canClimb = false;
    }
}

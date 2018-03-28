using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour {
    public bool canClimb;
    Rigidbody rb;
    public Animator anim;
    public Camera regularCam;
    public Camera parcourCam;
    public Animation climbAnim;
    bool isClimbing = false;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (canClimb && Input.GetKeyDown(KeyCode.Space) && !isClimbing)
        {
            isClimbing = true;
            regularCam.depth = 0;
            parcourCam.depth = 1;
            rb.isKinematic = true;
            anim.SetBool("Climbup",true);
            StartCoroutine(afterClimb());
        }
	}
    IEnumerator afterClimb(){
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Climbup" ,false);
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Climb"))
        {
            yield return null;
        }
        regularCam.depth = 1;
        parcourCam.depth = 0;
        rb.isKinematic = false;
        isClimbing = false;
        transform.position = parcourCam.transform.position;
    }
    void OnTriggerEnter (Collider other){
        canClimb = true;
    }
    void OnTriggerExit (Collider other){
        canClimb = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerBehaviour : MonoBehaviour {
    enum AniState{Idle,Walk,Climb}
    AniState CurAni;
    bool isClimbing = false;
    public bool canClimb;
	bool isTagger = true;
	Rigidbody rb;
	public float speed;
	float mouseSense= 3f;
    public Animator anim;

	void Awake () {
		Cursor.lockState = CursorLockMode.Locked;
		rb = GetComponent<Rigidbody> ();
	}
    void update(){

        switch (CurAni)
        {
            case (AniState.Idle):
                anim.SetBool("Walking", false);
                anim.SetBool("climbup",false);
                break;
            case (AniState.Walk):
                anim.SetBool("Walking", true);
                break;
            case (AniState.Climb):
                anim.SetBool("climbup", true);
                break;
                
        }

        if (canClimb && Input.GetKeyDown(KeyCode.Space) && !isClimbing)
            Debug.Log("thou shal climb");
            isClimbing = true;
            rb.isKinematic = true;
            anim.SetBool("Climbup", true);
            StartCoroutine(afterClimb());
        }


//    if(Input.GetKeyDown(KeyCode.W))
//        Debug.Log("work pls");
//        anim.SetBool("Walking",true);
    
    IEnumerator afterClimb(){
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Climbup", false);
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Climb2"))
        {
            yield return null;
        }
        rb.isKinematic = false;
        isClimbing = false;
    }
	void FixedUpdate () {
		float horMove = Input.GetAxis("Horizontal");
		float verMove = Input.GetAxis("Vertical");

        if(horMove != 0 || verMove != 0)
        rb.velocity = (transform.forward * verMove + transform.right * horMove)*speed;

		transform.Rotate (0, Input.GetAxis ("Mouse X") * mouseSense, 0f);
		transform.GetChild (0).Rotate (-Input.GetAxis("Mouse Y") * mouseSense,0f,0f);
	}
    void OnTriggerEnter (Collider other){
        canClimb = true;
    }
    void OnTriggerExit (Collider other){
        canClimb = false;
    }
}


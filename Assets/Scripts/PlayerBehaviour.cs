using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerBehaviour : MonoBehaviour {
    enum AniState{Idle,Walk,Climb}
    AniState curAni;
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
    void Start(){
        curAni = AniState.Idle;
    }
    void Update(){
        switch (curAni)
        {
            case (AniState.Idle):
                anim.SetBool("Walking",false);
                anim.SetBool("climbup",false);
                break;
            case (AniState.Walk):
                anim.SetBool("Walking", true);
                anim.SetBool("climbup",false);
                break;
            case (AniState.Climb):
                anim.SetBool("climbup", true);
                anim.SetBool("Walking",false);
                break;      
        }
        if (canClimb && Input.GetKeyDown(KeyCode.Space) && !isClimbing)
//            Debug.Log("thou shall climb");
        {
            isClimbing = true;
            rb.isKinematic = true;
            curAni = AniState.Climb;
            StartCoroutine(afterClimb());
            }
        if (Input.anyKey == false)
            anim.SetBool("Walking",false);
        }
    IEnumerator afterClimb(){
        yield return new WaitForSeconds(0.3f);
//        anim.SetBool("Climbup", false);
        curAni = AniState.Walk;
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

        if (horMove != 0 || verMove != 0)
            curAni = AniState.Walk;
        rb.velocity = (transform.forward * verMove + transform.right * horMove)*speed;
		rb.velocity = (transform.forward * verMove + transform.right * horMove) * speed;

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


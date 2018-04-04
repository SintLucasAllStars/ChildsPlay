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

	GameObject blinkObject;

	void Awake () {
		Cursor.lockState = CursorLockMode.Locked;
		rb = GetComponent<Rigidbody> ();

	}
    void Start(){
        CurAni = AniState.Idle;
    }
    void Update(){
//		switch (CurAni) {
//		case (AniState.Idle):
//			anim.SetBool ("Walking", false);
//			anim.SetBool ("climbup", false);
//			break;
//		case (AniState.Walk):
//			anim.SetBool ("Walking", true);
//			break;
//		case (AniState.Climb):
//			anim.SetBool ("climbup", true);
//			break;      
//		}
//		if (canClimb && Input.GetKeyDown (KeyCode.Space) && !isClimbing){
//			Debug.Log ("thou shall climb");
//		isClimbing = true;
//		rb.isKinematic = true;
//		anim.SetBool ("Climbup", true);
//		StartCoroutine (afterClimb ());
//		}


		if (Input.GetMouseButtonDown (0))
			blinkObject = Instantiate (Resources.Load<GameObject> ("Blink"));

		if(Input.GetMouseButton(0) && blinkObject != null){
			blinkObject.transform.rotation = transform.rotation;
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 10f))
				blinkObject.transform.position = hit.point;
			else
				blinkObject.transform.position = ray.GetPoint (10f);

			if (Input.GetMouseButtonDown (1)) {
				transform.position = blinkObject.transform.position;
				Destroy (blinkObject);
			}
		}
		if (Input.GetMouseButtonUp (0) && blinkObject != null)
			Destroy (blinkObject);
    }
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

        if (horMove != 0 || verMove != 0)
            CurAni = AniState.Walk;

		float whY = rb.velocity.y;
		Vector3 newVelocity = (transform.forward * verMove + transform.right * horMove)*speed;
		newVelocity.y = whY;

		rb.velocity = newVelocity;

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


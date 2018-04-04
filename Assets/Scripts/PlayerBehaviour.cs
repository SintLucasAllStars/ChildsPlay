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
	bool isBlinking = false;
	float blinkDist = 15f;

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


		if (Input.GetMouseButtonDown (0) && !isBlinking)
			blinkObject = Instantiate (Resources.Load<GameObject> ("Blink"));

		if(Input.GetMouseButton(0) && blinkObject != null && !isBlinking){
			blinkObject.transform.rotation = transform.rotation;
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			blinkObject.transform.GetChild (1).LookAt (transform);
			if (Physics.Raycast (ray, out hit, blinkDist))
				blinkObject.transform.position = hit.point;
			else
				blinkObject.transform.position = ray.GetPoint (blinkDist);

			if (Input.GetMouseButtonDown (1)) {
				StartCoroutine (Blink ());
			}
		}
		if (Input.GetMouseButtonUp (0) && blinkObject != null && !isBlinking)
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
	IEnumerator Blink(){
		isBlinking = true;
		Vector3 startpos = transform.position;
		float timer = 0;
		while (timer < 1f) {
			transform.position = Vector3.Lerp (startpos, blinkObject.transform.GetChild(0).position, timer);
			if (timer <= 0.33333f) {
				Time.timeScale = Mathf.Lerp (1f, 0.25f, timer * 3);
				Camera.main.fieldOfView = Mathf.Lerp (60,120f,timer*3);
			} else {
				Time.timeScale = Mathf.Lerp (0.25f, 1f, (timer - 0.33333f) * 1.5f);
				Camera.main.fieldOfView = Mathf.Lerp (120,60f,(timer-0.33333f)*1.5f);
			}	
			timer += Time.deltaTime;
			yield return 0;
		}
		Time.timeScale = 1f;
		Camera.main.fieldOfView = 60;
		transform.position = blinkObject.transform.position;

		Destroy (blinkObject);
		isBlinking = false;
	}

    void OnTriggerEnter (Collider other){
        canClimb = true;
    }
    void OnTriggerExit (Collider other){
        canClimb = false;
    }
}


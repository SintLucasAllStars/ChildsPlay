using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour {

	public float mousSensitivity;
	public Transform playerBoddy;
	Ray pickupCheck;
	RaycastHit hit;
	public float checkRange;
	public Text pickupText;
	float xAxisClamp= 0f;
	GameObject fork;
	Vector3 originalForkPosition;
	public float forkPulloutSpeed;
	int clicksTillPull = 10;
	bool forkPulled = false;
	float clickUpNow;
	GameObject stabber;

	void Awake(){
		Cursor.lockState = CursorLockMode.Locked;
		 
	}

	void Start (){
		stabber = GameObject.Find ("/player/Main Camera/Fork");
		stabber.SetActive(false);
		fork = GameObject.Find ("/forkInStone(Clone)/Fork");
		originalForkPosition = fork.transform.position;
	}

	void Update () {
		if (fork != null) {
			fork.transform.position = new Vector3 (originalForkPosition.x, originalForkPosition.y + ((10 - clicksTillPull) * forkPulloutSpeed), originalForkPosition.z);
		}
		if (!forkPulled && clicksTillPull < 10 && Time.time > clickUpNow) {
			clickUpNow = Time.time + 0.25f;
			clicksTillPull++;

		}
		
		pickupCheck = new Ray (transform.position, transform.forward);
		if (Physics.Raycast (pickupCheck, out hit, checkRange)) {
			
			if (hit.collider.CompareTag ("fork")) {
				
				//pickupText.text = "pak hem maar";
			} else{
				//pickupText.text = "";
			}
			if (Input.GetMouseButtonDown (0) && hit.collider.CompareTag ("fork")) {
				if (clicksTillPull <= 0) {
					stabber.SetActive(true);
					forkPulled = true;
					Destroy (fork);
				}
				clicksTillPull--;

			}
		}  else{
			//pickupText.text = "";
		}

		if (Input.GetKeyDown (KeyCode.F)) {
			Cursor.lockState = CursorLockMode.Locked;
		}

		float mouseX = Input.GetAxis ("Mouse X");
		float mouseY = Input.GetAxis ("Mouse Y");

		float rotAmountX = mouseX * mousSensitivity;
		float rotAmountY = mouseY * mousSensitivity;

		xAxisClamp -= rotAmountY;

		Vector3 targetRotCam = transform.rotation.eulerAngles;
		Vector3 targetRotBody = playerBoddy.rotation.eulerAngles;

		targetRotCam.x -= rotAmountY;
		targetRotCam.z = 0;
		targetRotBody.y += rotAmountX;

		if (xAxisClamp > 90) {
			xAxisClamp = 90;
			targetRotCam.x = 90;
		}
		else if (xAxisClamp < -90) {
			xAxisClamp = -90;
			targetRotCam.x = 270;
		}

		transform.rotation = Quaternion.Euler (targetRotCam);
		playerBoddy.rotation =  Quaternion.Euler (targetRotBody);

	}


}

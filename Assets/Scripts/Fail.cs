using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fail : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Enemy")){
			SceneManager.LoadScene(2);
			Debug.Log("Loaded Scene 2");
		}
	}
}
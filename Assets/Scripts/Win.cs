using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")){
		SceneManager.LoadScene(1);
		Debug.Log("Loaded Scene 1");
		}
	}
}
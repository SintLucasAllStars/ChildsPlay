using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour {
    public GameManager managerInstance;

    // Use this for initialization
    void Start () {
        managerInstance = GameObject.Find("Managers").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.W))
	    {
	        managerInstance.SoundCheck(this.transform.position, 1);
	        Debug.Log("pressed");


	    }
    }
}

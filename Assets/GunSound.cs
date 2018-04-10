using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSound : MonoBehaviour
{
    public GameManager managerInstance;
	// Use this for initialization
	void Start ()
	{
	    managerInstance = GameObject.Find("Managers").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {


	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	       managerInstance.SoundCheck(this.transform.position);
	        Debug.Log("pressed");


        }
    }
}

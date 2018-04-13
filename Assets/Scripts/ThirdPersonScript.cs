using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThirdPersonScript : MonoBehaviour {
    public int health = 4;
    public enum State { Walking, sneeking, running, hiding}
    public State state;
    public float speed;
    public float setSpeed;
    public int turningSpeed;
    public static float conspicuousness = 1;
    Rigidbody rb;
    public Text healthTxt;


#region  Variables for my part of the script.  #Jesper
    public GameObject hidingObject;

	bool foundHidingSpot;

	float range;
	
	GameObject target;
	GameObject emptyTarget;
	public Vector3 targetxyz;
	public Vector3 emtpyTargetxyz;


	//UI Elements
	public Text pressToText;

	RaycastHit hit;
#endregion
    
    

	// Use this for initialization
	void Start () {
        setSpeed = speed;
        rb = GetComponent<Rigidbody>();

#region My part of the Start(). #Jesper
        range = 12f;

        pressToText.text = " ";
#endregion

	}
	
	// Update is called once per frame
	void Update () {
        healthTxt.text = health.ToString();
        if(health == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        //rb.AddForce(-Vector3.up);
        switch (state)
        {
            case State.Walking:
                speed = setSpeed;
                conspicuousness = 1;
                break;
            case State.sneeking:
                speed = setSpeed / 2;
                conspicuousness = 0.5f;
                //speed = 4;
                break;
            case State.running:
                conspicuousness = 2;
                speed = setSpeed * 1.5f;
                break;
            case State.hiding:
                conspicuousness = 0.1f;
                speed = 0;
                break;
            default:
                break;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, speed * Time.deltaTime);

            //rb.velocity = transform.forward * speed;
            //rb.velocity = rb.velocity + Vector3.up * Physics.gravity.y;
            //rb.AddForce(transform.forward * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
           //rb.velocity = transform.forward * -speed;
            //rb.velocity = rb.velocity + Vector3.up * Physics.gravity.y;
            //rb.AddForce(transform.forward * -speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            //rb.velocity = transform.forward * -speed;
            //rb.velocity = rb.velocity + Vector3.up * Physics.gravity.y;
            //rb.AddForce(transform.forward * -speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            //rb.velocity = transform.forward * -speed;
            //rb.velocity = rb.velocity + Vector3.up * Physics.gravity.y;
            //rb.AddForce(transform.forward * -speed);
        }


        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            state = State.sneeking;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            state = State.Walking;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            state = State.running;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            state = State.Walking;
        }

        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Rotate(0, -turningSpeed * Time.deltaTime, 0);
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Rotate(0, turningSpeed * Time.deltaTime, 0);
        //}


#region  My part of the Update().  #Jesper 
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
		{
			CheckForHidingSpot();
		}
        if(hit.collider == null)
        {
            ResetUI();
        }
			
		//Moves to targets position when there is a target.		#Jesper
		if(target != null)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetxyz, speed * Time.deltaTime);
		}
		//When on hiding position, displays option to leave hiding spot.	#Jesper
		if (transform.position == targetxyz)
		{
            state = State.hiding;
			pressToText.text = "Press E to leave";
			if(Input.GetKey(KeyCode.E))
			{
				ResetHidingSpot();
				LeaveHidingSpot();
				ColliderOn();
                ResetUI();
			}
		}	
#endregion	
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health--;
        }
    }

    //Raycast that checks for hidingspots.	#Jesper
	void CheckForHidingSpot()
	{
			if (hit.transform.tag == "Hidingspot")
			{
				pressToText.text = "Press E to Hide";
				if(Input.GetKey(KeyCode.E))
				{
					Debug.Log(hit.transform.tag);
					hidingObject = hit.transform.gameObject;
					ColliderOff();
					SetHidingSpot();
				}
				
			}
			else
			{
				Debug.Log("Keep Looking");
			}
	}

#region My Methods. #Jesper
	//Sets hidingspot you see as target.	#Jesper
	void SetHidingSpot()
	{
		target = hidingObject;
		targetxyz = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
	}
	//Empties your target.	#Jesper
	void ResetHidingSpot()
	{
		
		target = emptyTarget;
		targetxyz = emtpyTargetxyz;
	}

	//Turns collider of the hidingspot off so "Player" can hide in the hidingspot.	#Jesper
	void ColliderOff()
	{
		Collider tempCollider;

		tempCollider = hidingObject.GetComponent<Collider>();

		tempCollider.enabled = false;
	}
	//Turns Collider of the hidingspot back on so "Player" can hide again in the hidingspot.	#Jesper
	void ColliderOn()
	{
		Collider tempCollider;

		tempCollider = hidingObject.GetComponent<Collider>();

		tempCollider.enabled = true;
	}
	
	//I tried making a bool of both the ColliderOff() and ColliderOn() methods but didn't manage to make it work//


	//leaves hidingspot. #Jesper
	void LeaveHidingSpot()
	{
        float x, y, z;

        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

		transform.position = new Vector3(x, y, z - 2f);
        
        state = State.Walking;
	}
    // Resets UI Elemnts. #Jesper
    void ResetUI()
    {
        pressToText.text = " ";
    }
#endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonScript : MonoBehaviour {

    enum State { Walking, sneeking, running}
    State state;
    public int speed;
    public int turningSpeed;
    public float conspicuousness = 1;
    Rigidbody rb;   

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //rb.AddForce(-Vector3.up);
        switch (state)
        {
            case State.Walking:
                //speed = 8;
                conspicuousness = 1;
                break;
            case State.sneeking:
                conspicuousness = 0.5f;
                //speed = 4;
                break;
            case State.running:
                //speed = 10;
                break;
            default:
                break;
        }
        if (Input.GetKey(KeyCode.W))
        {
            //rb.velocity = transform.forward * speed;
            //rb.velocity = rb.velocity + Vector3.up * Physics.gravity.y;
            rb.AddForce(transform.forward * speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //rb.velocity = transform.forward * -speed;
            //rb.velocity = rb.velocity + Vector3.up * Physics.gravity.y;
            rb.AddForce(transform.forward * -speed);
        }

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            state = State.Walking;
            rb.velocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            state = State.sneeking;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            state = State.Walking;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -turningSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, turningSpeed * Time.deltaTime, 0);
        }
    }
}

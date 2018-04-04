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

        switch (state)
        {
            case State.Walking:
                speed = 8;
                break;
            case State.sneeking:
                conspicuousness = 0.5f;
                speed = 4;
                break;
            case State.running:
                speed = 10;
                break;
            default:
                break;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * speed;
        } else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = transform.forward * -speed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            state = State.sneeking;
        }
        else
        {
            state = State.Walking;
            rb.velocity = Vector3.zero;
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

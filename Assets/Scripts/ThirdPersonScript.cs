using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThirdPersonScript : MonoBehaviour {
    public int health = 4;
    public enum State { Walking, sneeking, running}
    public State state;
    public float speed;
    public float setSpeed;
    public int turningSpeed;
    public static float conspicuousness = 1;
    Rigidbody rb;
    public Text healthTxt;
    
    

	// Use this for initialization
	void Start () {
        setSpeed = speed;
        rb = GetComponent<Rigidbody>();
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health--;
        }
    }
}

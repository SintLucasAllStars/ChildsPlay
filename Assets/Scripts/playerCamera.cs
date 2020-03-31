using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    //defining 

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0f;
    private float pitch = 0f;

    private Camera cam;

    // Use this for initialization
    // refer to camera

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    // If i move my mouse the camera turns and so does the player

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch, 0, 25);

        transform.eulerAngles = new Vector3(0f, yaw, 0f);
        cam.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }
}

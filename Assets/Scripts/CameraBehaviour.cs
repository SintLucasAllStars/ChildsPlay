using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    #region declaration
    private new Camera camera;
    private float maxFov;
    private float minFov;

    private float movementSpeed;
    #endregion

    void Start()
    {
        #region initialization
        //FOV variable initialization
        maxFov = 80;
        minFov = 40;
        //movement speed initialization
        movementSpeed = 0.1f;
        //finding camera and assigning it
        camera = FindObjectOfType<Camera>();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region camera Movement
        //declaration local movement variables
        float hMove;
        float vMove;

        //asingment
        hMove = Input.GetAxis("Horizontal");
        vMove = Input.GetAxis("Vertical");

        //actule movement
        transform.Translate(hMove * movementSpeed, 0, vMove * movementSpeed, Space.Self);
        #endregion

        #region camera Zoom
        //reading Key Input
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            //increasing FOV
            camera.fieldOfView++;
        }

        //reading Key Input
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            //decreasing FOV
            camera.fieldOfView--;
        }

        //locking the FOV
        camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, minFov, maxFov);
        #endregion
    }
}

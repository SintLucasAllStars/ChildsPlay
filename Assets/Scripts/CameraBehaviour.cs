using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Camera camera;
    private float maxFov = 80;
    private float minFov = 40;
    void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Failed attempt at locking the FOV
        Mathf.Clamp(camera.fieldOfView, minFov, maxFov);
        
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            camera.fieldOfView++;
        }

        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            camera.fieldOfView--;
        }
        
    }
}

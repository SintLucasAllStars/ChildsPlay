using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    bool active = true;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            active = !active;
        }

        if (active == true)
        {
            Follow();
        } else { }
    }

    void Follow()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = 23.5f; // Set this to be the distance you want the object to be placed in front of the camera.
        this.transform.position = Camera.main.ScreenToWorldPoint(temp);
    }
}

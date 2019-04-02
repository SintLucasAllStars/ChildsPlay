using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    float yAs;
    bool go;
    void Awake()
    {
        go = false;
    }

    // Update is called once per frame

    void Update()
    {
        MovementAnimation();
        RotationMovement();
    }

    void RotationMovement()
    {
        yAs -= 0.2f;
        transform.rotation = Quaternion.Euler(0, yAs, 0);
    }

    void MovementAnimation()
    {
        //if(gameObject.transform.position.y >= 1.5f)
        //{
        //    go = !go;
        //}
        //if(gameObject.transform.position.y <= 0f)
        //{
        //    go = !go;
        //}

        if(go == false)
        {
            transform.Translate(0, 0.01f, 0 * Time.deltaTime);
        }
        if (go == true)
        {
            transform.Translate(0, -0.01f, 0 * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BlokHigh")
        {
            go = true;
        }
        if (other.gameObject.tag == "BlokLow")
        {
            go = false;
        }
    }
}

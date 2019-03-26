using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    float yAs;
    void Start()
    {
    }

    // Update is called once per frame

    void Update()
    {
        yAs-= 0.2f;
        transform.rotation = Quaternion.Euler(0, yAs, 0);
    }
}

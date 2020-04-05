using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) { speed = 4.5f;  }
        if (Input.GetKeyUp(KeyCode.LeftControl)) { speed = 3f;  }
        if (Input.GetKey(KeyCode.W)) { transform.Translate(0, 0, speed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.A)) { transform.Rotate(0, -64 * Time.deltaTime, 0); }
        if (Input.GetKey(KeyCode.D)) { transform.Rotate(0, 64 * Time.deltaTime, 0); }
    }
}

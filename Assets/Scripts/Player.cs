using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float m_speed;

    public GameObject m_camera;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        
        // Vertical
        if (Input.GetKey(KeyCode.W))
        {
            //transform.Rotate(0, 270, 0, Space.World);
            transform.rotation = Quaternion.Euler(0, m_camera.transform.rotation.eulerAngles.y, 0);
            Move();

        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.Rotate(0, 270, 0, Space.World);
            transform.rotation = Quaternion.Euler(0, m_camera.transform.rotation.eulerAngles.y + 180, 0);
            Move();
        }

        // Horizontal
        if (Input.GetKey(KeyCode.D))
        {
            //transform.Rotate(0, 270, 0, Space.World);
            transform.rotation = Quaternion.Euler(0, m_camera.transform.rotation.eulerAngles.y + 90, 0);
            Move();
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Rotate(0, 90, 0, Space.World);
            transform.rotation = Quaternion.Euler(0, m_camera.transform.rotation.eulerAngles.y + 270, 0);
            Move();
        }
        
        
    }

    void Move()
    {
        //Debug.Log(m_camera.transform.rotation.y);
        Debug.Log("transform.rotation angles x: " + m_camera.transform.rotation.eulerAngles.x + " y: " + m_camera.transform.rotation.eulerAngles.y + " z: " + m_camera.transform.rotation.eulerAngles.z);
        transform.Translate(Vector3.forward * m_speed * Time.deltaTime);
    }
}

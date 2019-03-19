using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float m_speed;           // The speed at which the character moves
    public static float m_idle;     // How long the character has been idle for

    public GameObject m_camera;     // The camera

    // Start is called before the first frame update
    void Start()
    {
        m_camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        
        // Vertical Rotation
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0, m_camera.transform.rotation.eulerAngles.y, 0);
            Move();

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0, m_camera.transform.rotation.eulerAngles.y + 180, 0);
            Move();
        }

        // Horizontal Rotation
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, m_camera.transform.rotation.eulerAngles.y + 90, 0);
            Move();
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, m_camera.transform.rotation.eulerAngles.y + 270, 0);
            Move();
        }

        // Idle goes down
        m_idle -= 1 * Time.deltaTime;
        
    }

    void Move()
    {
        // Player always moves forward
        // As long as they're moving, idle is set to 1
        transform.Translate(Vector3.forward * m_speed * Time.deltaTime);
        m_idle = 1;
    }
}

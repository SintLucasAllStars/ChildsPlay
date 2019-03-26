using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLoc : MonoBehaviour
{

    public GameObject m_player;     // The player character, to grab values from

    public float m_distance;          // The distance between the player and the camera
    private float m_distanceOrigin;   // The distance set in the editor
    public int m_height;            // The height the camera is above the player

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("CameraLock");
        m_distanceOrigin = m_distance;
    }

    // Update is called once per frame
    void Update()
    {
        // The camera will always be looking at the direction of the player
        // The camera will also always make sure it is [m_distance] away from the player
        transform.LookAt(m_player.transform); 
        transform.position = (transform.position - m_player.transform.position).normalized * m_distance + m_player.transform.position;

        if(transform.position.y != m_player.transform.position.y + m_height)
        {
            // In case the camera is not at the correct height, fix it immediately.
            transform.position = new Vector3(transform.position.x, m_player.transform.position.y + m_height, transform.position.z);
        }

        // The camera can be turned left/right when pressing the keys
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Player.m_idle = 1;
            transform.RotateAround(m_player.transform.position, Vector3.down, 90);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Player.m_idle = 1;
            transform.RotateAround(m_player.transform.position, Vector3.up, 90);
        }

        // The camera can zoom in/out
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Player.m_idle = 1;
            if (m_distance > m_distanceOrigin / 1.5f)
            {
                m_distance = m_distance / 1.5f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Player.m_idle = 1;
            if(m_distance < m_distanceOrigin * 1.5f)
            {
                m_distance = m_distance * 1.5f;
            }
        }

        // If the player has been idle for too long, the camera will rotate around the player
        // 
        if (Player.m_idle <= 0)
        {
            transform.RotateAround(m_player.transform.position, Vector3.down, 20 * Time.deltaTime);
        }
    }
}

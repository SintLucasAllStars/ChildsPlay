using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //player object
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region player input
        if (Input.GetKey(KeyCode.W))
        {
            player.transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.position += new Vector3(0, 0, -1) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.position += new Vector3(0, 0, 1) * Time.deltaTime;
        }
        #endregion
    }
}

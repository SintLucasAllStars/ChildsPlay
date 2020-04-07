using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //player call
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        //player movement
        if (Input.GetKey(KeyCode.W))
        {
            player.transform.position += new Vector3(0, 0, 1) * 10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.position += new Vector3(-1, 0, 0) * 10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.transform.position += new Vector3(0, 0, -1) * 10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.position += new Vector3(1, 0, 0) * 10 * Time.deltaTime;
        }

        #region player boundaries
        if (player.transform.position.x > 19.25f)
        {
            player.transform.position = new Vector3(19.25f, player.transform.position.y, player.transform.position.z);
        }
        if (player.transform.position.x < -19.25f)
        {
            player.transform.position = new Vector3(-19.25f, player.transform.position.y, player.transform.position.z);
        }
        if (player.transform.position.z > 12.5f)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 12.5f);
        }
        if (player.transform.position.z < -10.5f)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10.5f);
        }
        #endregion
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Cop"))
        {
            SceneManager.LoadScene("Controls");
        }
        
    }
}

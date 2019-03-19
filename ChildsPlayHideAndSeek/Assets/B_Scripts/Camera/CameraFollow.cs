using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject player;

    private Vector3 offSetPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Slime");
        offSetPos = new Vector3(0, 15, -2);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position + offSetPos; ;
    }
}

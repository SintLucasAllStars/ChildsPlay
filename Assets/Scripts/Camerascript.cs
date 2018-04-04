using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerascript : MonoBehaviour {

    public GameObject player;
    Vector3 distance = new Vector3(0, 8, 0);

    private void Start()
    {

    }

    void FixedUpdate () {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + distance, 0.3f);
	}
}

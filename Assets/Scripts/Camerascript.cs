using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerascript : MonoBehaviour {

    public GameObject player;
    Vector3 vieuwDistance = new Vector3(0, 20, 0);

    private void Start()
    {

    }

    void FixedUpdate () {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + vieuwDistance, 0.3f);
	}
}

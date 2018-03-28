using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
	public GameObject bullet;
	public GameObject bulletSpawnPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Mouse0)) {
			ShootBullet ();
		}
	}

	void ShootBullet(){
		Instantiate (bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
	}
}

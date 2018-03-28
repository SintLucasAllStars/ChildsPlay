﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
	public GameObject bullet;
	public GameObject bulletSpawnPoint;
	float timeBetweenShots;
	float timeBetweenShotsDefault;
	public AudioSource gunShot;

	// Use this for initialization
	void Start () {
		timeBetweenShotsDefault = 0.3f;
		timeBetweenShots = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Mouse0)) {
			ShootBullet ();
		}

		ReduceTimer ();

	}

	void ShootBullet(){
		if (timeBetweenShots <= 0f) {
			gunShot.Play ();
			Instantiate (bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
			timeBetweenShots = timeBetweenShotsDefault;
		}
	}

	void ReduceTimer(){
		timeBetweenShots = timeBetweenShots - Time.deltaTime;

	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunWeapon : MonoBehaviour
{

    public float range = 50f;
    private Camera playerCam;
    public float fireRate;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private LineRenderer laserLine;
    private float nextFire;
    public Transform gunEnd;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = Camera.main;
        laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)//Makes the player shoot his stungun only when the cooldown is over
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());//calls method for visual laser line

            Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));//the raycast is casted from the center of the screen
            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);//visual laser effect
            if(Physics.Raycast(rayOrigin,playerCam.transform.forward, out hit, range))//checks if the raycast hits an enemy
            {
                laserLine.SetPosition(1, hit.point);
                NpcMovement target = hit.transform.GetComponent<NpcMovement>();//gets refrence towards other script
                if(target != null)
                {
                    target.TakeStunEffect();//calls method in other script. To activate stun effect
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (playerCam.transform.forward * range));//if no npc was hit. 
            }

        }
    }

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}

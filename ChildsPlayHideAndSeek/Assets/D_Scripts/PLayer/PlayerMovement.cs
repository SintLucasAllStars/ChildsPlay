﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region Character stuff

    //Object related stuff
    public GameObject Mouth001_Obj, Mouth002_Obj;
    public GameObject Character_Obj;
    public Rigidbody Character_RB;

    //rotation stuff
    public Transform Target_Looking;

    //Movement stuff
    public Transform Target_Moving;
    private float speed_Movement = 100;
    #endregion

    #region Shooting stuff
    public GameObject Ball_Pfb;
    public Transform SpawnLocation;
    private bool canShoot;
    #endregion

    void Start()
    {
        Mouth001_Obj.name = "Normal";
        Mouth002_Obj.name = "Shooting";
    }

    void Awake()
    {
        Mouth001_Obj.SetActive(true);
        Mouth002_Obj.SetActive(false);
        canShoot = true;
    }

    void Update()
    {
        Shooting();
        Looking();
        //Moving();
    }


    void Looking()
    {
        //Always looking at the mouse:
        if (Target_Looking != null)
        {
            transform.LookAt(Target_Looking);
        }
    }

    void Moving()
    {
          transform.position = Vector3.MoveTowards(Target_Moving.transform.position, Target_Moving.transform.position, Time.deltaTime * speed_Movement);
    }

    void Shooting()
    {
        if (canShoot == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(IsShooting());
            }
        }
    }


    IEnumerator IsShooting()
    {
        canShoot = false;
        Mouth001_Obj.SetActive(false);
        Mouth002_Obj.SetActive(true);
        Instantiate(Ball_Pfb, SpawnLocation.transform.position, SpawnLocation.transform.rotation);
        yield return new WaitForSeconds(1);
        Mouth001_Obj.SetActive(true);
        Mouth002_Obj.SetActive(false);
        yield return new WaitForSeconds(1);
        canShoot = true;

    }
}

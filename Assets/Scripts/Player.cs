﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject gun;
    public GameObject gunDropPrefab;
    public GameObject bulletPrefab;
    public Transform shootOffset;



    public bool hit = false; //this bool should be changed by the bullet script
    public float dropRange;

    public bool hasShot;
    public GameObject _droppedgun;

    // Start is called before the first frame update
    void Start()
    {
        gun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * dropRange), Color.black);

        if (Input.GetMouseButtonDown(0) && gun.activeSelf == true && !hasShot)
        {
            Debug.Log("Player shooting");
            Instantiate(bulletPrefab, shootOffset.position, shootOffset.rotation);
            hasShot = true;

            Invoke("DropWeapon", 2);
        }

    }

    public void DropWeapon()
    {
        Debug.Log("Dropped weapon");
        _droppedgun = Instantiate(gunDropPrefab, shootOffset.position, transform.rotation);

        Rigidbody rb = _droppedgun.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 1000);

        //calling ActivateDropWeapon
        //Invoke("ActivateDropWeapon", 0.5f);

        gun.SetActive(false);
        hasShot = false;
    }

    void ActivateDropWeapon()
    {
        if (_droppedgun != null)
        {
            _droppedgun.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    void Die()
    {
        Debug.Log("Player died");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Gun") && Input.GetKey(KeyCode.E))
        {
            Debug.Log("Picked up gun");
            Destroy(other.gameObject);
            gun.SetActive(true);
        }

        if (other.CompareTag("Bullet") && gun.activeInHierarchy == false)
        {
            Die();
        }
    }

    public void RecieveWeapon()
    {
        Debug.Log(gameObject.name + "got weapon");
        //get weapon
    }

}

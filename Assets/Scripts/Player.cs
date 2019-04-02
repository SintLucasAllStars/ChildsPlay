using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : Person
{
    public bool hit = false; //this bool should be changed by the bullet script
    public float dropRange;

    private void Start()
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
            Shoot();
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Gun") && Input.GetKey(KeyCode.E))
        {
            Debug.Log("Picked up gun");
            EquipWeapon(other.gameObject);
        }
        if (other.gameObject.CompareTag("Bullet") && gun.activeInHierarchy == false)
        {
            SceneManager.LoadScene("Loss");
        }
    }
}

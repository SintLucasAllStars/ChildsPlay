using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{

    public GameObject gun;
    public GameObject bulletPrefab;
    public Transform shootOffset;

    public bool hit = false; //this bool should be changed by the bullet script
    public float dropRange;


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

            StartCoroutine(DropWeapon(shootOffset.position, gun));
        }

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
            Die(gameObject);
        }
    }

    public void RecieveWeapon()
    {
        Debug.Log(gameObject.name + "got weapon");
        //get weapon
    }

}

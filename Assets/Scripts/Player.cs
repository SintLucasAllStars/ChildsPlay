using System.Collections;
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

    bool hasShot;
    GameObject _droppedgun;

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
            //DropWeapon();
            Debug.Log("Player shooting");
            Instantiate(bulletPrefab, transform.position, shootOffset.rotation);
            hasShot = true;

            Invoke("DropWeapon", 2);
        }

    }

    public void DropWeapon()
    {
        Debug.Log("Dropped weapon");
        _droppedgun = Instantiate(gunDropPrefab, transform.position, transform.rotation);

        //GameObject droppedgun = Instantiate(gun, transform.position, transform.rotation);
        //droppedgun.AddComponent<Rigidbody>();
        //droppedgun.GetComponent<MeshCollider>().convex = true;
        Rigidbody rb = _droppedgun.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * 10);
        Invoke("ActivateDropWeapon", 1);

        gun.SetActive(false);
        hasShot = false;
    }

    void ActivateDropWeapon()
    {
        _droppedgun.GetComponent<BoxCollider>().isTrigger = true;
    }
    
    void Die()
    {
        Debug.Log("Player died");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            Destroy(other.gameObject);
            gun.SetActive(true);
        }

        if (other.CompareTag("Bullet") && gun.activeInHierarchy == false)
        {
            Die();
        }
    }


}

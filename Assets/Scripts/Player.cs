using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gun;
    public GameObject bulletPrefab;
    public Transform shootOffset;
    public bool hit; //this bool should be changed by the bullet script

    bool hasShot;

    // Start is called before the first frame update
    void Start()
    {
        gun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gun.activeSelf == true)
        {
            Debug.Log("Player shooting");
            Instantiate(bulletPrefab, transform.position, shootOffset.rotation);
            hasShot = true;
        }

        if (hasShot)
        {
            if(GameObject.FindGameObjectsWithTag("Bullet") == null)
            {
                Debug.Log("Found no bullets");
                hasShot = false;

                if (hit)
                {
                    hit = false;
                }
                else
                {
                    DropWeapon();
                }
            }
        }
    }

    void DropWeapon()
    {
        //drop weapon
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            Destroy(other.gameObject);
            gun.SetActive(true);
        }
    }


}

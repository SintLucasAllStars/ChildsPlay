using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public bool hasShot;

    public GameObject gunDropPrefab;
    public GameObject gun;
    public Transform shootOffset;
    public GameObject bulletPrefab;

    public IEnumerator DropWeapon(Vector3 dropOffset, GameObject g)
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Dropped weapon");
        GameObject droppedgun = Instantiate(gunDropPrefab, dropOffset, transform.rotation);

        Rigidbody rb = droppedgun.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 1000);

        //calling ActivateDropWeapon
        //Invoke("ActivateDropWeapon", 0.5f);

        g.SetActive(false);
        hasShot = false;
    }

    public void RecieveWeapon()
    {
        Debug.Log(gameObject.name + "got weapon");
        gameObject.tag = "Seeker";
        //get weapon
    }

    public void Die(GameObject person)
    {
        Debug.Log(person.name +  " died");
        Gamemanager gm = GameObject.FindObjectOfType<Gamemanager>().GetComponent<Gamemanager>();

        gm.CheckDeath(person);
        Destroy(person);
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, shootOffset.position, shootOffset.rotation);
        hasShot = true;

        StartCoroutine(DropWeapon(shootOffset.position, gun));
    }

    public void EquipWeapon(GameObject gunOnGround)
    {
        Destroy(gunOnGround);
        gun.SetActive(true);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && gun.activeInHierarchy == false)
        {
            Die(gameObject);
        }
    }
}

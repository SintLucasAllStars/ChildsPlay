using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{

    public GameObject droppedgun;
    public GameObject gunDropPrefab;
    public bool hasShot;

    public IEnumerator DropWeapon(Vector3 dropOffset, GameObject g)
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Dropped weapon");
        droppedgun = Instantiate(gunDropPrefab, dropOffset, transform.rotation);

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
        //get weapon
    }

    public void Die(GameObject person)
    {
        Debug.Log(person.name +  " died");
        Gamemanager gm = GameObject.FindObjectOfType<Gamemanager>().GetComponent<Gamemanager>();

        gm.CheckDeath(person);
        Destroy(person);
    }
}

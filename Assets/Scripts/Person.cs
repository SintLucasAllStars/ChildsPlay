using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{

    public GameObject droppedgun;
    public GameObject gunDropPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropWeapon()
    {
        Debug.Log("Dropped weapon");
        droppedgun = Instantiate(gunDropPrefab, shootOffset.position, transform.rotation);

        Rigidbody rb = droppedgun.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 1000);

        //calling ActivateDropWeapon
        //Invoke("ActivateDropWeapon", 0.5f);

        gun.SetActive(false);
        hasShot = false;
    }

    public void RecieveWeapon()
    {
        Debug.Log(gameObject.name + "got weapon");
        //get weapon
    }
}

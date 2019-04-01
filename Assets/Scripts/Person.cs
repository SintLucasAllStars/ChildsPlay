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

    //public void DropWeapon(Vector3 dropOffset, GameObject g, bool shot)
    //{
    //    Debug.Log("Dropped weapon");
    //    droppedgun = Instantiate(gunDropPrefab, dropOffset, transform.rotation);

    //    Rigidbody rb = droppedgun.GetComponent<Rigidbody>();
    //    rb.AddForce(transform.forward * 1000);

    //    //calling ActivateDropWeapon
    //    //Invoke("ActivateDropWeapon", 0.5f);

    //    g.SetActive(false);
    //    shot = false;
    //}

    public IEnumerator DropWeapon(Vector3 dropOffset, GameObject g, bool shot)
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Dropped weapon");
        droppedgun = Instantiate(gunDropPrefab, dropOffset, transform.rotation);

        Rigidbody rb = droppedgun.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 1000);

        //calling ActivateDropWeapon
        //Invoke("ActivateDropWeapon", 0.5f);

        g.SetActive(false);
        shot = false;

    }

    public void RecieveWeapon()
    {
        Debug.Log(gameObject.name + "got weapon");
        //get weapon
    }
}

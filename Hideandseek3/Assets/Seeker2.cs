using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker2 : MonoBehaviour
{
    RaycastHit hit;

    
    void Start()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 10,transform.position.z);
        }
    }

 
    void Update()
    {
       // Vector3 v, 


        if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            if (hit.collider.gameObject.CompareTag("Hider"))
            {
                //   Debug.Log("Gevonden");
                hit.transform.SendMessage("HitByRay");
            }
        }
    }
}

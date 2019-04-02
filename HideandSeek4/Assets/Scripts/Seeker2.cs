using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker2 : MonoBehaviour
{
    RaycastHit hit;


    public Light zaklamp;

    public int aanuit = 0;

    void Start()
    {

        zaklamp.intensity = 0;
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

        if (Input.GetKeyUp(KeyCode.F))
        {



            if (aanuit % 2 == 0)
            {
                zaklamp.intensity = 3;
            }

            if (aanuit % 2 == 1)
            {
                zaklamp.intensity = 0;
            }

            aanuit++;

        }
    }
}

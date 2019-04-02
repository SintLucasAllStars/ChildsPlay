using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptje : MonoBehaviour
{
    public Transform seekertje;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {

            //playercoll = true;

            transform.position = Vector3.MoveTowards(transform.position, seekertje.position, 1);
          //  Debug.Log("collie");
        }

    }
}

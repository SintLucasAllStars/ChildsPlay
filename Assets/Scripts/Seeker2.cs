using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seeker2 : MonoBehaviour
{
    RaycastHit hit;
    RaycastHit Hithit;

    public int score2;
    public Text score1;

    public Light zaklamp;

    public int aanuit = 0;


    void Start()
    {
        score2 = 0;
        zaklamp.intensity = 0;
    }

 
    void Update()
    {
        // Vector3 v, 

        score1.text = "Score: " + score2;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            if (hit.collider.gameObject.CompareTag("Hider"))
            {
               
                //   Debug.Log("Gevonden");
                hit.transform.SendMessage("HitByRay");
            }
        }

        if (Physics.Raycast(transform.position, transform.forward, out Hithit, 10))
        {
            if (Hithit.collider.gameObject.CompareTag("Hider"))
            {

                //   Debug.Log("Gevonden");
                Hithit.transform.SendMessage("FoundByPlayer");
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

        if(score2 == 5)
        {
            score1.text = "DONE";
        }
       
    }

    public void scoreding()
    {
        score2 = score2 + 1;
        score1.text = "Score: " + score2;
    }
}

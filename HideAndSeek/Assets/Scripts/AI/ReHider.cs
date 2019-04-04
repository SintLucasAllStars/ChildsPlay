using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReHider : Humanoid
{
    private AEnvQuerySystem EQS;
    private GameObject Seeker;

    private bool seekerSeen = false;

    private void Start()
    {
        EQS = AEnvQuerySystem.Instance;
        Seeker = GameObject.FindGameObjectWithTag("Seeker");
        FindNewLocation();
    }

    private void Update()
    {
        SimpleSight();
        
        if (seekerSeen)
        {
            StartCoroutine(RunAwayDelay());
        }

       //Debug.Log("SeekerSeen = " + seekerSeen);
    }

    private void FindNewLocation()
    {
        Debug.Log(EQS.hideLocations.Count);
        int index = Random.Range(0, EQS.hideLocations.Count);
        Vector3 newLoc = EQS.hideLocations[index].GetWorldLocation();
        MoveTo(newLoc);
    }

    //OldFunction

    //private void LookForSeeker()
    //{
    //    Ray ray = new Ray(transform.position + new Vector3(0, 1, 0), (Seeker.transform.position - transform.position));
      
    //        if (Physics.Raycast(ray, out RaycastHit hit, 20))
    //        {
    //            if (hit.collider.CompareTag("Seeker"))
    //            {
    //                seekerSeen = true;
                    
    //                RunAway();
    //            }
    //            else
    //            {
    //                seekerSeen = false;
                    
    //            }
    //        }
        
    //}

    private void RunAway()
    {
        Vector3 distToSeeker = transform.position - Seeker.transform.position;
        Vector3 newPos = transform.position + distToSeeker;

        agent.destination = newPos;
        
    }

    private void SimpleSight()
    {
        Vector3 playerDir = Seeker.transform.position - transform.position;

        float angle = Vector3.Angle(playerDir, transform.forward);

        if(angle >= -45 && angle <= 45)
        {
            Ray ray = new Ray(transform.position + new Vector3(0,1,0), playerDir);

            if(Physics.Raycast(ray, out RaycastHit hit, 10))
            {
                if (hit.collider.CompareTag("Seeker"))
                {
                    seekerSeen = true;
                }
                else
                {
                    seekerSeen = false;
                }
            }
        }


    }

    IEnumerator RunAwayDelay()
    {
        RunAway();
        yield return new WaitForSeconds(4);
        FindNewLocation();
        seekerSeen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Seeker"))
        {
            Destroy(this.gameObject);
        }
    }

}

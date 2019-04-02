using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : Humanoid
{
    public AEnvQuerySystem EQSSystem;

    GameObject Seeker;

    // Start is called before the first frame update
    void Start()
    {
       Seeker = GameObject.FindGameObjectWithTag("Seeker");
        FindNewLocation();
        EQSSystem = AEnvQuerySystem.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            FindNewLocation();
        }
        LookForSeeker();
    }

    void FindNewLocation()
    {
        int index = Random.Range(0, EQSSystem.hideLocations.Count);
        Debug.Log(EQSSystem.hideLocations.Count);

        Vector3 newLoc = EQSSystem.hideLocations[index].GetWorldLocation();
        MoveTo(newLoc);
    }

    void LookForSeeker()
    {
        bool seekerSeen = false;
        Ray ray = new Ray(transform.position + new Vector3(0,1,0), (Seeker.transform.position - transform.position) );
        if (seekerSeen == false)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 10))
            {
                if (hit.collider.CompareTag("Seeker"))
                {
                    seekerSeen = true;
                    FindNewLocation();
                }
                else
                {
                    seekerSeen = false;
                }
            }
        }
    }

}


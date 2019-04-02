using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : Humanoid
{
    List<EnvQueryItem> hideLocations = new List<EnvQueryItem>();
    public EnvQuerySystem EQSSystem;

    GameObject Seeker;

    // Start is called before the first frame update
    void Start()
    {
       Seeker = GameObject.FindGameObjectWithTag("Seeker");
        FindNewLocation();
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
        foreach (EnvQueryItem item in EQSSystem.QueryItems)
        {
            if (item.EnemyNearby == false && item.IsColliding == false)
            {
                hideLocations.Add(item);
            }
        }
        MoveTo(hideLocations[Random.Range(0, hideLocations.Count)].GetWorldLocation());
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


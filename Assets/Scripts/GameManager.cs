using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public Transform SoundLocation;

    public GameObject[] hiders;

    public List<EnemyAI> hiderAis;
	// Use this for initialization
	void Start ()
	{
	    hiders = GameObject.FindGameObjectsWithTag("Hider");
	    for (int i = 0; i < hiders.Length; i++)
	    {
	        hiderAis.Add(hiders[i].GetComponent<EnemyAI>());

        }

    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SoundCheck(Vector3 soundLocation)
    {
        int soundLevel;
        int soundBaseLevel = 0;
        int modifier = 0;
        float distanceToSound;
        Debug.Log("before for loop");
        for (int i = 0; i < hiders.Length; i++)
        {
            // baseSoundlevel based on distance

            distanceToSound = Vector3.Distance(soundLocation, hiders[i].transform.position);
            Debug.Log(distanceToSound + "Distance");
            if (distanceToSound >= 10)
            {
                soundBaseLevel = 5;
            }
            Debug.Log(soundBaseLevel);
            modifier = 1;
            soundLevel = soundBaseLevel * modifier;
            Debug.Log(soundLevel);
            // change mode of hider based on soundlevel

            if (soundLevel >= 4)
            {
                hiderAis[i].StupidWorkAround();
                Debug.Log("got past mode switch");
            }
        }
        Debug.Log("after for loop");




    }
}

using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<EnemyAI> hiderAis;

    public GameObject[] hiders;

    public Transform SoundLocation;

    // Use this for initialization
    private void Start()
    {
        hiders = GameObject.FindGameObjectsWithTag("Hider");
        for (var i = 0; i < hiders.Length; i++)
        {
            hiderAis.Add(hiders[i].GetComponent<EnemyAI>());

        }
    }

    // Update is called once per frame
    private void Update()
    {
    }


    /// <summary>
    /// SoundCheck checks the loudness of the sound based on distance and type of sound.
    /// (soundLevel = soundBaseLevel * modifier)
    ///
    /// adding additional sounds is easy make a if statement with as condition the Soundlevel and specify at which level what action the ai needs to take.  
    /// 
    /// Examples at the bottom of SoundCheck
    /// 
    /// right now we only have 2 outcomes.
    /// 1. normal noise or not a loud noise (that is still heard) ai just runs to a different hiding spot
    /// 2. loud noise ai runs really fast to a different hiding spot
    /// </summary>
    public void SoundCheck(Vector3 soundLocation, int modif)
    {
        int soundLevel;
        var soundBaseLevel = 0;
        var modifier = 0;

        modifier = modif;


        float distanceToSound;
        Debug.Log("before for loop");
        for (var i = 0; i < hiders.Length; i++)
        {
            // baseSoundlevel based on distance

            distanceToSound = Vector3.Distance(soundLocation, hiders[i].transform.position);
            Debug.Log(distanceToSound + "Distance");

            // when sound is far away
            if (distanceToSound <= 200 && distanceToSound > 60) soundBaseLevel = 1;
            // When sound is Close
            if (distanceToSound <= 60) soundBaseLevel = 2;
            if (distanceToSound > 200) soundBaseLevel = 0;

            //basesoundlevel is amplified with de modifier or type of sound like Gunfire or footsteps
            soundLevel = soundBaseLevel * modifier;
            Debug.Log(soundLevel);
            // change mode of hider based on soundlevel

            // if a loud noise is heard 
            if (soundLevel >= 5) hiderAis[i].HearingLoudNoise();

            // if a normal noise is heard
            if (soundLevel >= 2 && soundLevel < 5) hiderAis[i].HearingNormalNoise();
        }

        Debug.Log("after for loop");
    }
    
}
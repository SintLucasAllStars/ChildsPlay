using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour
{
    public AudioSource yayKids, Spit, Music;

    bool globalAudio;
    bool audioVolume;
    public void Awake()
    {
        globalAudio = true;
        audioVolume = true;
    }
    public void GlobalAudio()
    {
        globalAudio = !globalAudio;
        if(globalAudio == true)
        {
            yayKids.volume = 0.6f;
            Spit.volume = 1;
        }
        if(globalAudio == false)
        {
            yayKids.volume = 0;
            Spit.volume = 0;
            Music.volume = 0;
        }
    }

    public void Audio()
    {
        audioVolume = !audioVolume;
        if(audioVolume == true)
        {
            Music.volume = 0.3f;
        }
        if(audioVolume == false)
        {
            Music.volume = 0;
        }
    }
}

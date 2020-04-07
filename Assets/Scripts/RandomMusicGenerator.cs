using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusicGenerator : MonoBehaviour
{
    public List<GameObject> music;
    private int random;
    private GameObject selected;

    private void Update()
    {
        if (Scenemanagement.instance.music == true)
        {
            Debug.Log("doe iets");
            Scenemanagement.instance.music = false;
            random = Random.Range(0, music.Count);
            selected = music[random];
            Instantiate(selected);
        }

        //Debug.Log(Scenemanagement.instance.music);
    }
}

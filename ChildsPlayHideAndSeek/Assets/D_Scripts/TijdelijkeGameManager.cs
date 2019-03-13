using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TijdelijkeGameManager : MonoBehaviour
{
    //cameras
    public Camera maincam, playercamview;
    void Start()
    {
        maincam.enabled = true;
        playercamview.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            maincam.enabled = !maincam.enabled;
            playercamview.enabled = !playercamview.enabled;
        }
    }
}

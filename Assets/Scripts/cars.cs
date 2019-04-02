using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cars : MonoBehaviour
{




    float minFlickerSpeed = 0.05f;
    float maxFlickerSpeed = 0.4f;

    public Light licht;
    public Light licht2;

    void Start()
    {
        StartCoroutine(lichtje());

    }


    void Update()
    {



    }
    IEnumerator lichtje()
    {
        for (int i = 0; i < 100; i = i + 1)
        {

            licht.enabled = true;
            licht2.enabled = false;
            yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
            licht.intensity = Random.Range(1, 2);
            licht2.intensity = Random.Range(1, 2);


            licht.enabled = false;
            licht2.enabled = true;
            yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
            licht.intensity = Random.Range(1, 2);
            licht2.intensity = Random.Range(1, 2);


        }
    }
}
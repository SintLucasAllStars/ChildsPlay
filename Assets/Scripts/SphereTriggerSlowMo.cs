using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SphereTriggerSlowMo : MonoBehaviour
{
    public bool slowMoStart;




    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Bullet"))
        {
            Time.timeScale = 0.15f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            slowMoStart = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;

            slowMoStart = false;
        }
    }

}

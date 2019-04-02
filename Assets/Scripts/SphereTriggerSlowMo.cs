using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SphereTriggerSlowMo : MonoBehaviour
{

    public Slider slowMoBar;
    public bool slowMoStart;

    // Start is called before the first frame update
    void Start()
    {
        slowMoBar = GameObject.FindObjectOfType<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(slowMoBar != null)
        {
            if (!slowMoStart)
            {
                slowMoBar.value += Time.unscaledDeltaTime;
            }
            else
            {
                slowMoBar.value -= Time.unscaledDeltaTime;
            }

            if (slowMoBar.value <= 0)
            {
                //Debug.Log("No slowmo left");
            }
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Bullet"))
        {
            Time.timeScale = 0.25f;
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

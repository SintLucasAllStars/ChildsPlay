using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TestSlowMo : MonoBehaviour
{
    public Slider slowMoBar;
    public bool slowMoStart;

    // Start is called before the first frame update
    void Start()
    {
        slowMoBar = GameObject.FindObjectOfType<Slider>();
        slowMoStart = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            SlowMo(false);
            slowMoStart = true;
        }
        else
        {
            SlowMo(true);
            slowMoStart = false;
        }

        if(slowMoBar.value < 0)
        {
            SlowMo(false);
        }

    }

    void SlowMo(bool b)
    {
        if (b)
        {
            Time.timeScale = 0.25f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            slowMoBar.value -= Time.unscaledDeltaTime;
        }
        else
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;

            slowMoBar.value += Time.unscaledDeltaTime;



        }
    }
}

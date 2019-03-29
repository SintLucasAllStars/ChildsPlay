using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance;

    public static int m_day = 0; // The current cycle/wave, is used to calculate things like amount of customers and time

    // Start is called before the first frame update
    void Start()
    {


        // Check if instance already exists
        if(m_instance == null)
        {
            m_instance = this;
        }
        else if(m_instance != this)
        {
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

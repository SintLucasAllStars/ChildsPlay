using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Text m_time;
    public Text m_peopleText;

    public float m_miliseconds;
    public float m_seconds;
    public float m_minutes;

    public GameObject[] m_peopleList;
    public int m_peopleAmount;

    private AudioSource m_aud;
    public AudioClip m_pizza;

    public bool m_lose;

    // Start is called before the first frame update
    void Start()
    {
        m_aud = GetComponent<AudioSource>();
        m_aud.clip = m_pizza;
        m_aud.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_miliseconds < 0 && m_lose == false)
        {
            m_miliseconds = 1;

            if(m_seconds <= 0) // If Seconds is equal/lower than 0, remove a minute. Else make Second go down.
            {
                if (m_minutes < 1) // If Minute is equal/lower than q as well, lose. Else make Minute go down.
                {
                    Debug.Log("Player Lost");
                    m_lose = true;
                }
                else
                {
                    m_seconds = 59;
                    m_minutes--;
                }
            }
            else
            {
                m_seconds--;
            }
        }
        else
        {
            if(m_minutes == 0 && m_seconds == 0 && m_miliseconds > 0 && m_lose == false)
            {
                m_aud.pitch = m_miliseconds;
            }
            m_miliseconds -= Time.deltaTime * 1;
        }

        // Update the timer
        if(m_minutes < 10 && m_seconds < 10)
        {
            m_time.text = "0" + m_minutes.ToString() + ":0" + m_seconds.ToString();
        }
        else if(m_minutes < 10 && m_seconds > 9)
        {
            m_time.text = "0" + m_minutes.ToString() + ":" + m_seconds.ToString();
        }
        else if (m_minutes > 9 && m_seconds < 10)
        {
            m_time.text =  m_minutes.ToString() + ":0" + m_seconds.ToString();
        }
        else if (m_minutes > 9 && m_seconds > 9)
        {
            m_time.text = m_minutes.ToString() + ":" + m_seconds.ToString();
        }

        // Update the amount of customers left
        m_peopleList = GameObject.FindGameObjectsWithTag("Customer");
        m_peopleAmount = m_peopleList.Length;
        m_peopleText.text = m_peopleAmount.ToString();

    }
}

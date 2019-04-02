using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{

    public Text m_time; // The Timer on screen, displays how much time is left
    public Text m_peopleText; // Display how many people are left to catch

    public Slider m_slider; // Fancy slider underneath m_timer

    public float m_totalTime; // The total amount of time the player is given (in seconds)
    private float m_miliseconds; // Miliseconds, used to calculate the passing of time
    private float m_seconds; // The amount of displayed given seconds
    private float m_minutes; // The amount of displayed given minutes

    private GameObject[] m_peopleList; // Array of all the customers on the map
    public int m_peopleAmount; // The total number of customers/people that haven't been caught

    private AudioSource m_aud; // Audio Source
    public AudioClip m_pizza; // The infamous Pizza Theme

    // Start is called before the first frame update
    void Start()
    {
        // Setting the time
        m_totalTime = 30 + Mathf.Round(GameManager.m_day / 2) * 9 + Random.Range(0, 17);

        // Playing the background music
        m_aud = GetComponent<AudioSource>();
        m_aud.clip = m_pizza;
        m_aud.Play();

        // Setting the slider max value
        m_slider.maxValue = m_totalTime;

        // Converting the total time to minutes & seconds
        m_minutes = Mathf.Floor(m_totalTime / 60);
        m_seconds = m_totalTime - m_minutes * 60;

    }

    // Update is called once per frame
    void Update()
    {

        m_slider.value = m_totalTime;

        if(m_miliseconds < 0 && GameManager.m_lost == false)
        {
            m_miliseconds = 1;

            if(m_seconds <= 0) // If Seconds is equal/lower than 0, remove a minute. Else make Second go down.
            {
                if (m_minutes < 1) // If Minute is equal/lower than q as well, lose. Else make Minute go down.
                {
                    GameManager.m_lost = true;
                    SceneManager.LoadScene("Hotel");
                }
                else
                {
                    m_seconds = 59;
                    m_minutes--;
                    m_totalTime--;
                }
            }
            else
            {
                m_seconds--;
                m_totalTime--;
            }
        }
        else
        {
            // Music fades out as the timer reaches 0
            if(m_minutes == 0 && m_seconds == 0 && m_miliseconds > 0 && GameManager.m_lost == false)
            {
                m_aud.pitch = m_miliseconds;
            }
            m_miliseconds -= Time.deltaTime * 1;
        }

        // Update the timer display
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

        // If there are no customers left, 
        // the player wins and is loaded into a different scene
        if(m_peopleAmount <= 0)
        {
            GameManager.m_won = true;
            SceneManager.LoadScene("Pizzaria");
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{

    private int m_azizLine; // The line that Mr. Aziz will say
    private int m_peterLine; // The line that Peter will say
    private int m_sentence; // The current sentence that is being said (in order)

    private AudioSource m_aud;
    public AudioClip[] m_azizClips; // The voice clips of Mr. Aziz at the start of missions
    public AudioClip[] m_azizWon; // The voice clips of Mr. Aziz at the end of missions
    public AudioClip[] m_peterClips; // The voice clips of Peter at the start of missions
    public AudioClip[] m_peterWon; // The voice clips of Peter at the end of missions
    public AudioClip[] m_peterLost; // The voice clips of Peter when failing
    public AudioClip m_womanLost; // The voice clip of the woman when failing

    private GameObject m_cam; // Camera will turn towards the person speaking
    private GameObject m_aziz;
    private GameObject m_peter;
    private GameObject m_woman;

    public Text m_day;
    public Image m_background;

    // Start is called before the first frame update
    void Start()
    {
        m_background.color = new Color(1f, 1f, 1f, 0f);

        m_aud = GetComponent<AudioSource>();
        m_aud.volume = 1;

        m_peter = GameObject.Find("Peter");
        if (GameManager.m_lost == false)
        {
            m_aziz = GameObject.Find("Aziz");
        }
        else
        {
            m_woman = GameObject.Find("asuka");
            m_aziz = GameObject.Find("Peter_Scream");
            m_aziz.transform.position = new Vector3(1.75f, 2.15f, 3.5f);
        }

        // Deciding what lines Mr. Aziz and Peter will say during the cutscene
        if (GameManager.m_won == false && GameManager.m_lost == false)
        {
            GameManager.m_day++;
            m_azizLine = Random.Range(0, m_azizClips.Length);
            m_peterLine = Random.Range(0, m_peterClips.Length);
            m_sentence = -1;
        }
        else
        {
            m_azizLine = Random.Range(0, m_azizWon.Length);
            m_peterLine = Random.Range(0, m_peterWon.Length);
            m_sentence = 0;
        }

        if (GameManager.m_lost == false)
        {
            transform.LookAt(m_aziz.transform);
        }
        else
        {
            transform.position = new Vector3(0.9f, 1.7f, -2.35f);
            transform.LookAt(m_peter.transform);
            m_aud.clip = m_peterLost[0];
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if(m_sentence== -1 && m_aud.isPlaying == false)
        {
            m_background.color = new Color(1f, 1f, 1f, 1f);
            m_day.text = "Day\n" + GameManager.m_day.ToString();
            m_aud.volume = 0; // Audio only "plays" just so there is a delay between this action and the next
            m_aud.clip = m_peterLost[0];
            m_aud.Play();
            m_sentence = 0;
        }

        if(m_sentence == 0 && m_aud.isPlaying == false)
        {
            m_background.color = new Color(1f, 1f, 1f, 0f);
            m_day.text = "";
            m_aud.volume = 1;
            // Mr. Aziz is talking (Intro/Win) or Peter says Pizza Time (Lose)
            if (GameManager.m_won == true)
            {
                m_aud.clip = m_azizWon[m_azizLine];
            }
            else if(GameManager.m_won == false && GameManager.m_lost == false)
            {
                m_aud.clip = m_azizClips[m_azizLine];
            }
            m_aud.Play();
            m_sentence = 1;
        }

        if(m_sentence == 1 && m_aud.isPlaying == false)
        {
            if(GameManager.m_lost == false)
            {
                // Peter replies to Mr Aziz (Intro/Win)
                transform.LookAt(m_peter.transform);
                if (GameManager.m_won == false)
                {
                    m_aud.clip = m_peterClips[m_peterLine];
                }
                else
                {
                    m_aud.clip = m_peterWon[m_peterLine];
                }
            }
            else
            {
                // Woman says Peter is late (Lose)
                transform.position = new Vector3(1.7f, 1.4f, -0.2f);
                transform.LookAt(m_woman.transform);
                m_aud.clip = m_womanLost;
            }

            m_aud.Play();
            m_sentence = 2;
        }

        if(m_sentence == 2 && m_aud.isPlaying == false && GameManager.m_lost == false)
        {
            if(GameManager.m_won == false)
            {
                // The player is teleported to the City (Intro)
                SceneManager.LoadScene("City");
            }
            else
            {
                // The player goes on to the next day (Win)

                // The game reloads the pizzaria scene after winning
                GameManager.m_won = false;
                SceneManager.LoadScene("Pizzaria");

                /*
                m_background.color = new Color(1f, 1f, 1f, 1f);
                m_day.text = "Day\n" + GameManager.m_day.ToString();
                m_aud.volume = 0; // Audio only "plays" just so there is a delay between this action and the next
                m_aud.Play();
                m_sentence = 3;
                */
            }
        }
        else if (m_sentence == 2 && m_aud.isPlaying == false && GameManager.m_lost == true)
        {
            // Peter is screaming (Lose)
            m_aziz.transform.position = new Vector3(1.75f, 2.15f, -0.5f);
            transform.position = new Vector3(0.9f, 1.7f, -2.25f);
            transform.LookAt(m_peter.transform);
            m_aud.clip = m_peterLost[1];
            m_aud.Play();
            m_sentence = 3;
        }

        // The section where the game over image is shown (Lose)
        if (m_sentence == 3 && m_aud.isPlaying == false && GameManager.m_lost == true)
        {
            m_background.color = new Color(1f, 1f, 1f, 1f);
            m_aud.clip = m_peterLost[2];
            m_aud.Play();
            m_sentence = 4;
        }
        else if (m_sentence == 3 && m_aud.isPlaying == false && GameManager.m_won == true)
        {
            // The game reloads the pizzaria scene after winning
            GameManager.m_won = false;
            SceneManager.LoadScene("Pizzaria");
        }

        // Player is teleported back to the title (Lose)
        if (m_sentence == 4 && m_aud.isPlaying == false)
        {
            SceneManager.LoadScene("Title");
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{

    private int m_azizLine;
    private int m_peterLine;

    private bool m_azizSpoken;
    private bool m_peterSpoken;

    private AudioSource m_aud;
    public AudioClip[] m_azizClips;
    public AudioClip[] m_peterClips;

    private GameObject m_cam;
    private GameObject m_aziz;
    private GameObject m_peter;

    // Start is called before the first frame update
    void Start()
    {
        m_azizLine = Random.Range(0, m_azizClips.Length);
        m_peterLine = Random.Range(0, m_peterClips.Length);

        // I want this bool to go to false everytime this scene is loaded
        m_azizSpoken = false; 
        m_peterSpoken = false;

        m_aud = GetComponent<AudioSource>();

        m_aziz = GameObject.Find("Aziz");
        m_peter = GameObject.Find("Peter");

        m_aud.clip = m_azizClips[m_azizLine];

        transform.LookAt(m_aziz.transform);

    }

    // Update is called once per frame
    void Update()
    {
        
        if(m_azizSpoken == false && m_aud.isPlaying == false)
        {
            m_aud.Play();
            m_azizSpoken = true;
        }

        if(m_azizSpoken == true && m_peterSpoken == false && m_aud.isPlaying == false)
        {
            transform.LookAt(m_peter.transform);
            m_aud.clip = m_peterClips[m_peterLine];
            m_aud.Play();
            m_peterSpoken = true;
        }

        if(m_azizSpoken == true && m_peterSpoken == true && m_aud.isPlaying == false)
        {
            SceneManager.LoadScene("City");
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNoise : MonoBehaviour
{
    private float m_noiseTimer;     // Used to play noise every now and then

    private bool m_playing;

    private AudioSource m_aud;      // The audio component, used to create sound
    public AudioClip[] m_noises;    // Random noises the player will hear


    // Start is called before the first frame update
    void Start()
    {

        m_aud = GetComponent<AudioSource>();

        m_noiseTimer = Random.Range(1, 10);

    }

    // Update is called once per frame
    void Update()
    {

        if (m_noiseTimer > 0 && m_playing == false)
        {
            m_noiseTimer -= 1 * Time.deltaTime;
        }
        else if (m_playing == false)
        {
            int i = Random.Range(0, m_noises.Length);
            m_aud.clip = m_noises[i];
            m_aud.Play();
            m_playing = true;
        }

        if (m_aud.isPlaying == false && m_playing == true)
        {
            m_noiseTimer = Random.Range(2, 10);
            m_playing = false;
        }

    }
}

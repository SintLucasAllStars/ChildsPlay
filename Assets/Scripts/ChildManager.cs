using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildManager : MonoBehaviour
{
    //list van de kinderen
    private List<GameObject> m_Children = new List<GameObject>();

    // public GameObject m_prefab;
    // public GameObject m_KermitPrefab;
    public GameObject[] m_customers;

    private Vector3 m_randomVector;

    public float count; // Amount of customers that are spawned in

    void Start()
    {

        count = 1 + Mathf.Round(GameManager.m_day / 2) + Random.Range(0, 3);

        SpawnKinderen();

    }

  
    void Update()
    {
      


    }

    //houd de kinderen aantall
    public void ChildrenCount() {

        count = m_Children.Count;
        Debug.Log(count);

    }
    private void SpawnKinderen()
    {

        for (int i = 0; i < count; i++) // Repeat this equal to the amount of customers you want
        {

            int c = Random.Range(0, m_customers.Length); // Generate a random number equal to amount of prefabs

            m_randomVector = new Vector3(Random.Range(-70, 50), -0.125f, Random.Range(-50, 60)); // Set random location
            GameObject g = Instantiate(m_customers[c], m_randomVector, transform.rotation); // Spawn in the random prefab
            m_Children.Add(g); // Add to array

        }

        /*
        for (int i = 0; i < 5; i++)
        {

            m_randomVector = new Vector3(Random.Range(-70, 50), 1, Random.Range(-50, 60));
            GameObject g = Instantiate(m_prefab, m_randomVector, transform.rotation);
            m_Children.Add(g);

        }
        for (int i = 0; i < 7; i++)
        {

            m_randomVector = new Vector3(Random.Range(-70, 50), 1, Random.Range(-50, 60));
            GameObject g = Instantiate(m_KermitPrefab, m_randomVector, transform.rotation);
            m_Children.Add(g);

        }
        */

    }
    /*
    public void IncreaseSpeed()
    {
        int happend = 0;
        happend += 1;
        switch (happend)
        {
            case 0:
                foreach (GameObject G in m_Children)
                {

                    G.GetComponent<Children>().m_speed = 0.03f;
                }
                break;
            case 1:
                foreach (GameObject G in m_Children)
                {

                    G.GetComponent<Children>().m_speed = 0.04f;
                }
                break;
            case 2:
                foreach (GameObject G in m_Children)
                {

                    G.GetComponent<Children>().m_speed = 0.05f;
                }
                break;
            case 3:
                foreach (GameObject G in m_Children)
                {

                    G.GetComponent<Children>().m_speed = 0.06f;
                }
                break;
            case 4:
                foreach (GameObject G in m_Children)
                {

                    G.GetComponent<Children>().m_speed = 0.07f;
                }
                break;
            default:
                foreach (GameObject G in m_Children)
                {

                    G.GetComponent<Children>().m_speed = 0.04f;
                }
                break;
        }
      

    }
    */





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildManager : MonoBehaviour
{
    //list van de kinderen
    private List<GameObject> m_Children = new List<GameObject>();
    public GameObject m_prefab;
    public GameObject m_KermitPrefab;
    private Vector3 m_randomVector;
    private int count;

    void Start()
    {
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
        for (int i = 0; i < 5; i++)
        {

            m_randomVector = new Vector3(Random.Range(-6, 6), -3.87f, Random.Range(-6, 6));
            GameObject g = Instantiate(m_prefab, m_randomVector, transform.rotation);
            m_Children.Add(g);


        }
        for (int i = 0; i < 7; i++)
        {

            m_randomVector = new Vector3(Random.Range(-6, 6), -3.87f, Random.Range(-6, 6));
            GameObject g = Instantiate(m_KermitPrefab, m_randomVector, transform.rotation);
            m_Children.Add(g);


        }
    }
    public void IncreaseSpeed()
    {
        int happend;
        happend = 0;
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






}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildManager : MonoBehaviour
{
    //list of the customers
    private List<GameObject> m_Children = new List<GameObject>();

    public GameObject[] m_customers; // Customers

    private Vector3 m_randomVector;

    public float count; // Amount of customers that are spawned in

    void Start()
    {

        count = 1 + Mathf.Round(GameManager.m_day / 2) + Random.Range(0, 3);

        SpawnKinderen();

        SpawnCitizens();

    }

  
    void Update()
    {
      


    }

    // Holds the customer amount
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
    }

    private void SpawnCitizens()
    {

    }
}

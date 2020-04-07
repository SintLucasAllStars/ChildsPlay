using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseSpawnScript : MonoBehaviour
{
    public List<GameObject> Spawnpoints;
    public GameObject Cheese;
    public GameObject[] SpawnedCheese;
    public GameObject pickedSpawnpoint;
    private int rand;

    //# SINGLETON #
    public static CheeseSpawnScript instance { get; private set; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
       Spawnpoints = new List< GameObject > (GameObject.FindGameObjectsWithTag("Spawn"));
    }


    void Update()
    {
        SpawnedCheese = GameObject.FindGameObjectsWithTag("Cheese");

        //# IF ALL THE CHEESE ARE PICKED UP THAN INSTANTIATE 5 NEW ONCE #
        if (SpawnedCheese.Length <= 0)
        {
            for (int i = 0; i < 5; i++)
            {
                //Debug.Log("test");
                rand = Random.Range(0, Spawnpoints.Count);
                pickedSpawnpoint = Spawnpoints[rand];
                GameObject NewCheese = Instantiate(Cheese) as GameObject;
                NewCheese.transform.position = pickedSpawnpoint.transform.position;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Maneger : MonoBehaviour
{

    [SerializeField] private Transform SpwanPoint;
    public int AmountOfEnemys;
    public GameObject Enemies;
    public Terrain terrain;

    public GameObject[] Obstacles = new GameObject[3];

    // Use this for initialization
    void Start()
    {
        initSpawn();
        Vector3 width = terrain.terrainData.size;
        for (int i = 0; i < AmountOfEnemys; i++)
        {
            Instantiate(Obstacles[Random.Range(0, 3)], new Vector3(Random.Range(0, width.x), 0, Random.Range(0, width.x)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void initSpawn()
    {
        for (int i = 0; i < AmountOfEnemys; i++)
        {
            Vector3 temp = new Vector3(SpwanPoint.position.x, SpwanPoint.position.y, SpwanPoint.position.z + 1);
            int x = Random.Range(0, 250);
            int z = Random.Range(0, 250);
            Instantiate(Enemies, temp, Quaternion.identity);
        }
    }
}

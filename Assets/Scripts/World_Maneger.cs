using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Maneger : MonoBehaviour
{

    [SerializeField] private Transform SpwanPoint;
    public int AmountOfEnemys;
    public int AmountOfObstacles;
    public GameObject Enemies;
    public Terrain terrain;
    Vector3 width;

    public GameObject[] Obstacles = new GameObject[3];

    // Use this for initialization
    void Start()
    {

        width = terrain.terrainData.size;
        PlayerSpawn();
        ObstacleSpwan();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayerSpawn()
    {
        for (int i = 0; i < AmountOfEnemys; i++)
        {
            Vector3 temp = new Vector3(SpwanPoint.position.x, SpwanPoint.position.y, SpwanPoint.position.z + 1);
            Instantiate(Enemies, temp, Quaternion.identity);
        }
    }

    void ObstacleSpwan()
    {
        for (int i = 0; i < AmountOfObstacles; i++)
        {
            Instantiate(Obstacles[Random.Range(0, 3)], new Vector3(Random.Range(0, width.x), 0, Random.Range(0, width.x)), Quaternion.identity);
        }
    }
}

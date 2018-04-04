using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private void Start()
    {
        SpawnEnemies();
        StartCoroutine(ItemSpawner());
    }


    void SpawnEnemies()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int a = 0; a < 3; a++)
            {
                Instantiate(enemyPrefab, transform.position = new Vector3(1 + i + i, -2.33f + a + a, 0), Quaternion.identity);
            }
        }
    }

    IEnumerator ItemSpawner()
    {
        yield return new WaitForSeconds(Random.Range(15, 20));
        Instantiate(powerupPrefab, new Vector3(Random.Range(-8.3f, 8.3f), Random.Range(-4.31f, 4.31f), 0), Quaternion.identity);
    }
}

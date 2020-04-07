using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public static NpcManager Instance { get; set; }
    public GameObject enemy;
    public Transform[] spawnPoints;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            GameObject obj = Instantiate(enemy, spawnPoints[randomSpawnPoint].position, Quaternion.identity);

            obj.transform.parent = this.gameObject.transform;
        }

    }

    public void AlertMessage()
    {
        BroadcastMessage("StartChase");
        StartCoroutine("Timer");
        print("Started the timer");
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(4);
        BroadcastMessage("StopChase");
    }
}

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
        for (int i = 0; i < 8; i++)//spawns 8 instances of the npc prefab
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);//spawns npc on random spawnpoint in the map
            GameObject obj = Instantiate(enemy, spawnPoints[randomSpawnPoint].position, Quaternion.identity);//instantiate the enemy

            obj.transform.parent = this.gameObject.transform;//sets npc as child of the GameManager gameobject in game.
        }

    }

    public void AlertMessage()//if one npc sees the player and an other npc is in range. the Alertmessage method is called. if alertmessage method is called: every npc in the game goes in to chase state.
    {
        BroadcastMessage("StartChase");
        StartCoroutine("Timer");
        print("Started the timer");
    }
    private IEnumerator Timer()//timer for the alertmessage method
    {
        yield return new WaitForSeconds(4);
        BroadcastMessage("StopChase");
    }
}

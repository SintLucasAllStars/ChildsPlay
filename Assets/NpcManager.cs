using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public static NpcManager Instance { get; set; }
    public GameObject enemy;
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
        for (int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(enemy, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
            //GetComponent<Renderer>().material.color = new Color(0, 255, 0);
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

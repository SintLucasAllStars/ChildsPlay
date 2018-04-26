using System.Collections.Generic;
using UnityEngine;

public class CentralIntellignece : MonoBehaviour
{
    public static CentralIntellignece instance;
    private List<GameObject> guards;

    public float range;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Use this for initialization
    private void Start()
    {
        guards = new List<GameObject>();
        var container = GameObject.Find("Guards");
        for (var i = 0; i < container.transform.childCount; i++)
        {
            var current = container.transform.GetChild(i).gameObject;
            guards.Add(current);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Alert(GameObject guard)
    {
        //Logic
        var neighbours = GetNeighbours(guard);

        for (var i = 0; i < neighbours.Length; i++)
        {
            //neighbours[i].GetComponent<EnemyAI>().Alert();
        }
    }

    private GameObject[] GetNeighbours(GameObject guard)
    {
        var inRange = new List<GameObject>();

        for (var i = 0; i < guards.Count; i++)
        {
            var current = guards[i];
            if (current != guard)
            {
                var distance = Vector3.Distance(guard.transform.position, current.transform.position);
                if (distance < range) inRange.Add(current);
            }
        }

        return inRange.ToArray();
    }
}
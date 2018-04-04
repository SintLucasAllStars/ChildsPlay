using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralIntellignece : MonoBehaviour {

	public static CentralIntellignece instance;

	public float range;
	List<GameObject> guards;


	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	// Use this for initialization
	void Start () {
		guards = new List<GameObject>();
		GameObject container = GameObject.Find("Guards");
		for(int i=0; i<container.transform.childCount; i++)
		{
			GameObject current = container.transform.GetChild(i).gameObject;
			guards.Add(current);
		}
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void Alert(GameObject guard)
    {
        //Logic
        GameObject[] neighbours = GetNeighbours(guard);

        for (int i = 0; i < neighbours.Length; i++)
        {
            //neighbours[i].GetComponent<EnemyAI>().Alert();
        }
    }

    GameObject[] GetNeighbours(GameObject guard)
    {
        List<GameObject> inRange = new List<GameObject>();

        for (int i = 0; i < guards.Count; i++)
        {
            GameObject current = guards[i];
            if (current != guard)
            {
                float distance = Vector3.Distance(guard.transform.position, current.transform.position);
                if (distance < range)
                {
                    inRange.Add(current);
                }
            }
        }

        return inRange.ToArray();
    }
}

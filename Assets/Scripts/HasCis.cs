
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HasCis : MonoBehaviour
{

    public GameObject[] hidePlaces = new GameObject[4];
    public Animation[] selected = new Animation[4];

    public int Lives = 2;

    Ray ray;
    RaycastHit hit;

    //public Camera cam;
    //public Transform goal;

    // Use this for initialization
    void Start()
    {
        //NavMeshAgent agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < hidePlaces.Length; i++)
            {
                if (hit.collider.gameObject == hidePlaces[i].gameObject)
                {
                    Debug.Log("This is a hide place");
                    selected[i].Play("Selected");
                    Debug.Log(hidePlaces[i]);

                    Debug.Log("Ben je verstopt in" + hidePlaces[Random.Range(0,4)]);
                }
            }
        }
    }

    public void Seeker()
    {
        Debug.Log(Random.Range(0, 4));
    }
}
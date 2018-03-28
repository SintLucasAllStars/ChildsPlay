using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasCis : MonoBehaviour
{

    public GameObject[] hidePlaces = new GameObject[4];

    Ray ray;
    RaycastHit hit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //hidePlaces = GameObject.FindGameObjectsWithTag("Hide");

        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < hidePlaces.Length; i++)
            {
                if (hit.collider.gameObject == hidePlaces[i].gameObject)
                {
                    Debug.Log("This is a hide place");
                }
            }
        }
    }
}
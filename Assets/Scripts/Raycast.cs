using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour {

    public GameObject[] hidePlaces = new GameObject[4];
    public Animation[] selected = new Animation[4];

    public float range = 100f;
    public Camera fpsCam;

    public Image chosenBlockimage;
    public Sprite chosenBlock;

    RaycastHit hit;

    // Use this for initialization
    void Start ()
    {
        chosenBlockimage = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
        Debug.DrawRay(transform.position, forward, Color.green);

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range) && Input.GetButtonDown("Fire1"))
        {
            for (int i = 0; i < hidePlaces.Length; i++)
            {
                if (hit.collider.gameObject == hidePlaces[i].gameObject)
                {
                    Debug.Log("This is " + hidePlaces[i] + " bring it back to the spawn.");
                    selected[i].Play("Selected");
                    chosenBlockimage.sprite = chosenBlock;
                }
            }
        }
    }
}

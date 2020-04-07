using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class MovementDoggo : MonoBehaviour
{

    GameObject dog, enemy, cube,  enemy2;
    public bool hasTurned;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
        dog = GameObject.Find("GermanShephardLowPoly");
        cube = GameObject.Find("Cube");
        enemy2 = GameObject.Find("Enemy2");

        enemy2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.05f / 3, 0, 0 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Turn") && hasTurned == false)
        {
            enemy2.SetActive(true);
            enemy.transform.Rotate(0, -180, 0);
            enemy.transform.position = (new Vector3(18.13001f, 0.2199998f, -7.020032f));
            dog.transform.Rotate(0, -180, 0);
            Invoke("ChangeBool", 2f);
        }
        else if(other.gameObject.CompareTag("Turn") && hasTurned)
        {
            dog.transform.Rotate(0, 180, 0);
            enemy2.SetActive(false);
            enemy2.transform.position = (new Vector3(-21.51f, 0.2199998f, -7.02f));
            enemy.SetActive(true);
            Invoke("ChangeBool", 2f);
        }
    }

    void ChangeBool()
    {
        switch (hasTurned)
        {
            case true:
                hasTurned = false;
                break;
            case false:
                enemy.SetActive(false);
                hasTurned = true;
                break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public static bool Red;

    void Start()
    {

    }

    void Awake()
    {
        Red = false;
    }


    void Update()
    {

    }

    public static void RedColor()
    {
            Debug.Log("You've purchased the color red!");
            Red = true;
    }
}

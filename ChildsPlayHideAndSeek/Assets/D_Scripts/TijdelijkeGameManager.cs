using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TijdelijkeGameManager : MonoBehaviour
{
    //cameras
    public Camera maincam, playercamview;
    public GameObject Shop, Inventory;
    public static bool shopCanvas_B, Inventory_B;
    void Start()
    {
        maincam.enabled = true;
        playercamview.enabled = false;
        Shop.SetActive(false);
        Inventory.SetActive(false);
        shopCanvas_B = false;
        Inventory_B = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        CameraStuff();
        CanvasTrueChecker();
    }

    void CanvasTrueChecker()
    {
        //Shop
        if(shopCanvas_B == true)
        {
            Shop.SetActive(true);
        }
        if(shopCanvas_B == false)
        {
            Shop.SetActive(false);
        }

        //Inventory
        if(Inventory_B == true)
        {
            Inventory.SetActive(true);
        }
        if(shopCanvas_B == false)
        {
            Inventory.SetActive(false);
        }
    }

    void CameraStuff()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            maincam.enabled = !maincam.enabled;
            playercamview.enabled = !playercamview.enabled;
        }
    }
}

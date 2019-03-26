using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TijdelijkeGameManager : MonoBehaviour
{
    //cameras
    public Camera maincam, playercamview;
    public GameObject Shop;
    public static bool shopCanvas_B;
    void Start()
    {
        maincam.enabled = true;
        playercamview.enabled = false;
        Shop.SetActive(false);
        shopCanvas_B = false;
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

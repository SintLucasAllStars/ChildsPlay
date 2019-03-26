using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public static int coins_Int;
    public Text coins_Text;
    public GameObject Red_Button, Blue_Button;
    public AudioSource happyKids;

    void Start()
    {
        coins_Int = 10;
        Red_Button.SetActive(true);
    }


    void Update()
    {
        coins_Text.text = "Coins: " + coins_Int.ToString();

        if(coins_Int <= 0)
        {
            coins_Int = 0;
        }
    }

    public void Buy_Blue()
    {
        if(coins_Int > 9 && InventoryScript.Blue == false)
        {
            InventoryScript.BlueColor();
            coins_Int -= 10;
            happyKids.Play();
            Blue_Button.SetActive(false);
        }
        if(coins_Int <= 9 && InventoryScript.Blue == false)
        {
            Debug.Log("Not enough coins!");
        }
        if(InventoryScript.Blue == true)
        {
            Debug.Log("You've already purchased this color! Check your inventory!");
        }
    }

    public void Buy_Red()
    {
        if (coins_Int > 9 && InventoryScript.Red == false)
        {
            happyKids.Play();
            InventoryScript.RedColor();
            coins_Int -= 10;
            Red_Button.SetActive(false);
        }
        if(coins_Int <= 9 && InventoryScript.Red == false)
        {
            Debug.Log("Not enough coins!");
        }
        if(InventoryScript.Red == true)
        {
            Debug.Log("You've already purchased this color! Check your inventory!");
        }
    }
}

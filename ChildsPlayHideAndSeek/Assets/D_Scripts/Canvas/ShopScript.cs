using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public static int coins_Int;
    public Text coins_Text;
    public GameObject Red_Button;

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

    public void Buy_Red()
    {
        if (coins_Int > 0 && InventoryScript.Red == false)
        {
            InventoryScript.RedColor();
            coins_Int -= 10;
            Red_Button.SetActive(false);
        }
        if(coins_Int <= 0 && InventoryScript.Red == false)
        {
            Debug.Log("Not enough coins!");
        }
        if(InventoryScript.Red == true)
        {
            Debug.Log("You've already purchased this color! Check your inventory!");
        }
    }
}

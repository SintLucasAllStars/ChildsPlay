using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public static bool Red, Blue;
    public GameObject InventoryCanvas, OptionsPannel_OBJ, CollorPannel_OBJ;
    public GameObject CollorRedButton, CollorBlueButton;
    public Renderer Player;

    void Start()
    {
        InventoryCanvas.SetActive (false);
        OptionsPannel_OBJ.SetActive(false);
        CollorPannel_OBJ.SetActive(false);
        CollorRedButton.SetActive(false);
        CollorBlueButton.SetActive(false);
        Player.material.color = Color.green;
    }

    void Awake()
    {
        Red = false;
        Blue = false;
    }


    void Update()
    {
        OpeningInventory();
        if (Red == true)
        {
            CollorRedButton.SetActive(true);
        }

        if(Blue == true)
        {
            CollorBlueButton.SetActive(true);
        }
    }

    public static void RedColor()
    {
            Debug.Log("You've purchased the color red!");
            Red = true;
    }

    public static void BlueColor()
    {
        Debug.Log("You've purchased the color blue!");
        Blue = true;
    }

    public void OpeningInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryCanvas.SetActive(!InventoryCanvas.activeSelf);
            PlayerMovement.Inventory();
        }
    }

    public void OptionsPannel()
    {
        InventoryCanvas.SetActive(false);
        OptionsPannel_OBJ.SetActive(true);
        CollorPannel_OBJ.SetActive(false);

    }

    public void CollorPannel()
    {
        InventoryCanvas.SetActive(false);
        OptionsPannel_OBJ.SetActive(false);
        CollorPannel_OBJ.SetActive(true);
    }

    public void PannelBackToInventory()
    {
        InventoryCanvas.SetActive(true);
        OptionsPannel_OBJ.SetActive(false);
        CollorPannel_OBJ.SetActive(false);
    }

    public void RedColorButton()
    {
        Player.material.color = Color.red;
    }

    public void BlueColorButton()
    {
        Player.material.color = Color.blue;
    }

    public void GreenColorButton()
    {
        Player.material.color = Color.green;
    }
}

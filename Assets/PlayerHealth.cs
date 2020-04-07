using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    //public static PlayerHealth PH; //this is a way that other scripts can change certain values in this script

    int health, healthLeft, checker;
    float timeLeft;
    Text healthText;
    public GameObject panel;
    private Camera cam;
    Text panelText;

    private void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        cam.enabled = false;
        healthText = GameObject.Find("HealthText").GetComponent<Text>(); //gets the gameobject with the name "HealthText"
        health = 3; //health is default 3
        panel.SetActive(false);
    }

    private void Update()
    {
        healthText.text = "Health: " + healthLeft; //updates the text in game and sets it to a specific sting and int
        healthLeft = health; //defines what value healthleft is

        if (healthLeft <= 0) //is the int is below 0 the text will display 0 and not -1 for example
        {
            healthLeft = 0;
        }

        if (checker == 1)
        {
            timeLeft -= 1 * Time.deltaTime; //countdown based on realtime
        }

        if (timeLeft <= 0)
        {
            timeLeft = 0; //is the int is below 0 the text will display 0 and not -1 for example
            checker = 0; //reset checker to 0
        }

        if (healthLeft == 0)
        {
            healthText.text = "Health: " + 0;
            this.gameObject.SetActive(false);//set gameobject false
            FirstPersonAIO.Instance.TurnOnCursor();//turns on cursor, so you can use the game over panel with your cursor
            cam.enabled = true;
            panel.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && checker != 1) //if there is a collision with a enemy than to the following
        {
            timeLeft = 3; //reset the timer
            checker = 1; //so the code will run once
            health -= 1; //subtract 1 from health
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            cam.enabled = true;
            panel.SetActive(true);
            panelText = GameObject.Find("Text").GetComponent<Text>();
            this.gameObject.SetActive(false);
            FirstPersonAIO.Instance.TurnOnCursor();
            panelText.text = "Congratulations u escaped the Facility";
        }
    }
    public void MainMenuBtn()
    {
        SceneManager.LoadScene("MainMenu");
        print("Main menu");
    }
    public void TryAgainBtn()//button for game over panel
    {
        SceneManager.LoadScene("House");
        print("Try again");
    }
    public void ExitBtn()//button for game over panel
    {
        Application.Quit();
        print("Quit game");
    }
}

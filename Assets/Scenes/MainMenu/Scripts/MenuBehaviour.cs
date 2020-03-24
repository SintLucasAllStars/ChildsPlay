using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    public void OnClickedStart() //loads scene with the Index "0-1-2-3-4"
    {
        print("start is pressed");
        SceneManager.LoadScene("House");
    }

    public void OnClickedQuit() //Quit the application
    {
        print("quit is pressed");
        Application.Quit();
    }
}

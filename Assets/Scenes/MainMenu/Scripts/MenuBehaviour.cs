using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{

    private Canvas canvas2;
    private Canvas canvas;

    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvas2 = GameObject.Find("Canvas2").GetComponent<Canvas>();
        canvas2.enabled = false;
    }
    public void OnClickedStart() //loads scene with the Index "0-1-2-3-4"
    {
        SceneManager.LoadScene("House");
    }

    public void OnClickedHTP()
    {
        canvas.enabled = false;
        canvas2.enabled = true;
    }

    public void OnClickedBackButton()
    {
        canvas2.enabled = false;
        canvas.enabled = true;
    }

    public void OnClickedQuit() //Quit the application
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scenemanagement : MonoBehaviour
{

    public static Scenemanagement instance { get; private set; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool music;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        music = false;
    }

    void Update()
    {
        if (Application.loadedLevelName == "Titlescreen")
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("Controls");
            }
        }

        if (Application.loadedLevelName == "Controls")
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("Game");
                music = true;
            }

            if (Input.GetKey(KeyCode.Return))
            {
                Application.Quit();
            }
        }

        if (Application.loadedLevelName == "Game")
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("Controls");
            }
        }
    }
}

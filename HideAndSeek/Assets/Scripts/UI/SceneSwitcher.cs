using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public Button btn;
    public string sceneName;
    public bool quitGame;

    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(OpenLevel);   
    }

    void OpenLevel()
    {
        if (!quitGame)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Application.Quit();
        }
    }
    
}

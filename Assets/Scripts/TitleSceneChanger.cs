using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneChanger : MonoBehaviour
{
    public string sceneToChangeTo;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneToChangeTo);
        }

    }
    public void Title()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{

    public Text m_highScore;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.m_won = false;
        GameManager.m_lost = false;
        GameManager.m_day = 0;

        if(GameManager.m_bestDay != 1)
        {
            m_highScore.text = "Haven't been late \nfor " + GameManager.m_bestDay + " Days!";
        }
        else
        {
            m_highScore.text = "Haven't been late \nfor 1 Day!";
        }
        

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Pizzaria");
        }

    }
}

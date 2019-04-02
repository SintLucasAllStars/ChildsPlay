using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public List<GameObject> allPlayers = new List<GameObject>();
    public Text UICount;
    public Text readyInfo;

    float startTimer = 3;
    bool playerReady;
    bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        #region starting the game
        if (!gameStarted)
        {
            if (Input.GetKey(KeyCode.R))
                playerReady = true;

            if (startTimer >= 0 && playerReady)
            {
                startTimer -= Time.unscaledDeltaTime;
                UICount.text = Mathf.RoundToInt(startTimer).ToString();

                Time.timeScale = 0;
            }
            else if (startTimer <= 0)
            {
                UICount.text = "";
                readyInfo.text = "";
                Time.timeScale = 1;
                gameStarted = true;
            }
            else
                Time.timeScale = 0;
        }
        #endregion


        if(allPlayers.Count < 1)
        {
            SceneManager.LoadScene("Lose");
        }
    }

    public void CheckDeath(GameObject person)
    {
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if(allPlayers[i] == person)
            {
                allPlayers.RemoveAt(i);
            }
        }
    }
}

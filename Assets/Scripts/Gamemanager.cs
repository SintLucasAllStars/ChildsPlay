using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
        allPlayers.Add(GameObject.FindGameObjectWithTag("Player"));
        allPlayers.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        allPlayers[Random.Range(0, allPlayers.Count)].SendMessage("RecieveWeapon");

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

        if(allPlayers.Count < 4)
        {
            for (int i = 0; i < allPlayers.Count; i++)
            {
                
                if(allPlayers[i].GetComponent<Player>() != null)
                {
                    var script = allPlayers[i].GetComponent<Player>();
                    script.RecieveWeapon();
                }
                else
                {
                    //might need to change
                    var script = allPlayers[i].GetComponent<AI>();
                    //script.RecieveWeapon();
                }


            }
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

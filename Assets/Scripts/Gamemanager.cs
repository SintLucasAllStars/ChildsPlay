using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    List<GameObject> allPlayers = new List<GameObject>();
    public Text UICount;
    public Text readyInfo;

    float startTimer = 3;
    bool playerReady;

    // Start is called before the first frame update
    void Start()
    {
        allPlayers.Add(GameObject.FindObjectOfType<Player>().GetComponent<GameObject>());
        allPlayers.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
            playerReady = true;

        if (startTimer >= 0 && playerReady)
        {
            startTimer -= Time.unscaledDeltaTime;
            UICount.text = Mathf.RoundToInt(startTimer).ToString();

            Time.timeScale = 0;
        }
        else if(startTimer <= 0)
        {
            UICount.text = "";
            readyInfo.text = "";
            Time.timeScale = 1;
        }
        else
            Time.timeScale = 0; 
    }
}

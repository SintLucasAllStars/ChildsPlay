using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<Types> allPlayers;
    public List<Types> tikkers;
    public List<Types> nietTikkers;

    public int numTikkers;

    public static GameManager instance = null;
    public bool pastCountdown;

    void Start () {

        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);



        allPlayers = new List<Types>();
        tikkers = new List<Types>();
        nietTikkers = new List<Types>();

        GameObject[] typeArray = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < typeArray.Length; i++)
        {
            allPlayers.Add(typeArray[i].GetComponent<Types>());
            nietTikkers.Add(typeArray[i].GetComponent<Types>());
        }

        //Number of seekers and random objects become seeker
        numTikkers = Random.Range(1, 4);
        for (int i = 0; i < numTikkers; i++)
        {
            int random = Random.Range(0, typeArray.Length);
            tikkers.Add(allPlayers[random]);
            Debug.Log(allPlayers[random].gameObject.name + " is tagged, woohoo");
        }

        //The seeker's type gets set to tikker
        for (int i = 0; i < tikkers.Count; i++)
        {
			tikkers [i].type = Types.Type.Tagged;
			Debug.Log ("Player " + tikkers[i].gameObject.name + " has type " + tikkers [i].type);
            tikkers[i].GetComponent<MeshRenderer>().material.color = Color.red;
        }

        for (int i = 0; i < tikkers.Count; i++)
        {
            nietTikkers.Remove(tikkers[i]);
        }

        for (int i = 0; i < nietTikkers.Count; i++)
        {
            nietTikkers[i].canWalk = true;
			nietTikkers [i].type = Types.Type.NotTagged;

            nietTikkers[i].GetComponent<MeshRenderer>().material.color = Color.green;
        }

        StartCoroutine(TikkerDelay());
    }

    IEnumerator TikkerDelay()
    {
        yield return new WaitForSeconds(3);
        pastCountdown = true;
        Debug.Log("3 seconds are up.");
        for (int i = 0; i < numTikkers; i++)
        {
            tikkers[i].canWalk = true;
            Debug.Log(tikkers[i].gameObject.name + " can now walk");
        }
    }
}

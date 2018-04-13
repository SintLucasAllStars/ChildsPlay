using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasManager : MonoBehaviour {

    public Text playerText;

    private void Start()
    {
        StartCoroutine(Startje());
    }

    IEnumerator Startje () {
        yield return new WaitForSeconds(0.5f);
        playerText.text = "You're " + GameObject.Find("Player").GetComponent<Types>().type + ".";
	}

	public void RemainingUntagged () {
		playerText.text = "There are " + GetComponent<GameManager>().nietTikkers.Count + " non-tagged players remaining and there are " + GetComponent<GameManager>().tikkers.Count + " tagged players";
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class Menu : MonoBehaviour
{

	public void Restart()
	{
		SceneManager.LoadScene("Game");
	}

	public void QuitGame()
	{
		Debug.Log("Quitting game...");
		Application.Quit();
	}
}

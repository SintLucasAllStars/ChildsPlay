using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public Transform[] spawnHiders;
	public Transform spawnSeeker;
	public Transform spawnTemp;

	public GameObject preHider;
	public GameObject preSeeker;
	public GameObject preTemp;

	private bool gameHasStarted = false;

	public GameObject[] hiders;

	#region Singleton + SetUp
	public static GameManager Instance;

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this);
		}

		Cursor.visible = false;
		StartCoroutine(Spawn());
	}
	#endregion

	private IEnumerator Spawn()
	{
		GameObject temp = Instantiate(preTemp, spawnTemp.position, Quaternion.identity) as GameObject;
		yield return new WaitForSeconds(1);
		for (int i = 0; i < spawnHiders.Length; i++)
		{
			Instantiate(preHider, spawnHiders[i].position, Quaternion.identity);
		}
		yield return new WaitForSeconds(14);
		Destroy(temp);
		Instantiate(preSeeker, spawnSeeker.position, Quaternion.identity);
		gameHasStarted = true;
	}

	private void Update()
	{
		if(gameHasStarted)
		{
			hiders = GameObject.FindGameObjectsWithTag("Hider");

			if (hiders.Length == 0)
			{
				Debug.Log("You Win");
				Cursor.visible = true;
                SceneManager.LoadScene("Victory");

			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public Transform[] spawnHiders;
	public Transform spawnSeeker;

	public GameObject preHider;
	public GameObject preSeeker;

	private bool gameHasStarted = false;

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

		StartCoroutine(Spawn());
	}
	#endregion

	private IEnumerator Spawn()
	{
		yield return new WaitForSeconds(1);
		for (int i = 0; i < spawnHiders.Length; i++)
		{
			Instantiate(preHider, spawnHiders[i].position, Quaternion.identity);
		}
		yield return new WaitForSeconds(14);
		Instantiate(preSeeker, spawnSeeker.position, Quaternion.identity);
		gameHasStarted = true;
	}

	private void Update()
	{
		if(gameHasStarted)
		{
			var hiders = GameObject.FindGameObjectsWithTag("Hider");

			if (hiders.Length == 0)
			{
				Debug.Log("You Win");
                SceneManager.LoadScene("Victory");

			}
		}
	}
}

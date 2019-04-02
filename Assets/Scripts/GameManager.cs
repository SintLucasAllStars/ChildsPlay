using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public GameObject prefab;
	public List<GameObject> Players = new List<GameObject>();
	public Text Timer;

	public float hidingSeconds;

	int playersNum = 3;

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(this);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		for (int i = 0; i < playersNum; i++)
		{
			Players.Add(prefab);
			
		}

		foreach (GameObject item in Players)
		{
			Instantiate(prefab);
		}

		 

	}

    // Update is called once per frame
    void Update()
    {


		hidingSeconds -= Time.deltaTime;
		Timer.text = hidingSeconds.ToString();

	}

	
}

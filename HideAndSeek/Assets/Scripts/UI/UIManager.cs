using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	#region Singleton

	private static UIManager instance;
	public static UIManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<UIManager>();
			}
			return instance;
		}
	}

	#endregion

	public Slider slider;
	public Text backText, frontText, countFrontText, countBackText;

	//slider variables
	private bool staminaChanging = false;
	private float curStamina = 100f;
	private float targetAmount;
	private float timer;
	private float cooldown = .4f;

	//text variables
	private float targetTime = 15f;
	private float myDeltaTime = 100f;

	private void Start()
	{
		//slider = GetComponentInChildren<Slider>();
		slider.value = curStamina;
		slider.gameObject.SetActive(false);
		countBackText.enabled = false;
		countFrontText.enabled = false;
	}

	private void Update()
	{
		if(staminaChanging)
			staminaChanging = ChangeStamina();

		if(myDeltaTime <= 0f)
		{
			backText.enabled = false;
			frontText.enabled = false;
			slider.gameObject.SetActive(true);
			countBackText.enabled = true;
			countFrontText.enabled = true;
			countBackText.text = GameManager.Instance.hiders.Length.ToString("0");
			countFrontText.text = GameManager.Instance.hiders.Length.ToString("0");
		}
		else
		{
			myDeltaTime = targetTime - Time.time;
			backText.text = myDeltaTime.ToString("0");
			frontText.text = myDeltaTime.ToString("0");
		}
	}

	public void OnStaminaChanged(float _stamina)
	{
		targetAmount = _stamina;
		staminaChanging = true;
	}

	private bool ChangeStamina()
	{
		timer += Time.deltaTime;
		if(timer <= cooldown)
		{
			slider.value = Mathf.Lerp(curStamina, targetAmount, timer / cooldown);
		}
		else
		{
			slider.value = targetAmount;
			curStamina = targetAmount;
			timer = 0f;
			return false;
		}
		return true;
	}
}

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

	private bool staminaChanging = false;
	private float curStamina = 100f;
	private float targetAmount;
	private float timer;
	private float cooldown = .4f;

	private void Start()
	{
		//slider = GetComponentInChildren<Slider>();
		slider.value = curStamina;
	}

	private void Update()
	{
		if(staminaChanging)
			staminaChanging = ChangeStamina();
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

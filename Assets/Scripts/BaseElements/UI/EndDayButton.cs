﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class EndDayButton : MonoBehaviour
{
	private BaseManager baseManager;


	private void Awake()
	{
		baseManager = FindObjectOfType<BaseManager>();
		Assert.IsNotNull(baseManager, "Missing baseManager");
		Button button = GetComponent<Button>();
		Assert.IsNotNull(button, "Missing button");
		button.onClick.AddListener(OnClicked);
	}

	private void OnClicked()
	{
		baseManager.BeginDayEnd();
	}
}
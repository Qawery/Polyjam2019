﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;


public class ActionButtonsManager : MonoBehaviour
{
	[SerializeField] private SelectActionButton selectActionButtonPrefab;
	private BaseManager baseState;
	private List<SelectActionButton> spawnedButtons = new List<SelectActionButton>();


	private void Awake()
	{
		Assert.IsNotNull(selectActionButtonPrefab);
		baseState = FindObjectOfType<BaseManager>();
		Assert.IsNotNull(baseState, "Missing baseState");
		baseState.OnActionChange += UpdateActions;
	}

	private void Start()
	{
		UpdateActions();
	}

	private void UpdateActions()
	{
		List<Action> availableActions = baseState.GetAvailableAction();
		while (spawnedButtons.Count > availableActions.Count)
		{
			spawnedButtons.RemoveAt(spawnedButtons.Count - 1);
		}
		while (spawnedButtons.Count < availableActions.Count)
		{
			SelectActionButton newButton = Instantiate(selectActionButtonPrefab);
			newButton.transform.parent = transform;
			spawnedButtons.Add(newButton);
		}
		int buttonIndex = 0;
		foreach (Action action in availableActions)
		{
			spawnedButtons[buttonIndex].SetAction(action);
			++buttonIndex;
		}
	}
}